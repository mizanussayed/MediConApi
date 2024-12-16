using MediCon.Api.Configurations.Enums;
using MediCon.Api.Configurations.Settings;
using MediCon.Core.Configurations.CommonModel;
using MediCon.Core.Configurations.Helpers;

using ImageMagick;

using Microsoft.Extensions.Options;

namespace MediCon.Api.Configurations.Helpers;

public class FileHelper
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IDateTimeHelper _dateTimeHelper;
    private readonly FileUploadSettings _fileUploadOptions;

    public const string PublicFolder = "uploads/public";
    public const string PrivateFolder = "uploads/protected";

    private static readonly string[] AllowedImageExtensions = [".png", ".jpg", ".jpeg", ".gif", ".avif", ".webp"];
    private static readonly string[] AllowedDocumentExtensions = [".doc", ".docx", ".xls", ".xlsx", ".pdf", ".txt"];
    private static readonly string[] AllowedVideoExtensions = [".mp4", ".mkv", ".webm"];

    public FileHelper(IWebHostEnvironment webHostEnvironment, IDateTimeHelper dateTimeHelper, IOptions<FileUploadSettings> fileUploadOptions)
    {
        _webHostEnvironment = webHostEnvironment;
        _dateTimeHelper = dateTimeHelper;
        _fileUploadOptions = fileUploadOptions.Value;
    }

    public string GetRootPath()
    {
        var fileUploadFolderPath = _fileUploadOptions.FileUploadFolderPath;
        var rootPath = string.IsNullOrEmpty(fileUploadFolderPath)
            ? Path.Combine(_webHostEnvironment.ContentRootPath, "..")
            : fileUploadFolderPath;

        return Path.GetFullPath(rootPath);
    }

    private string GetGeneratedFileName(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var currentDateTimeString = _dateTimeHelper.Now.ToString("yyyyMMddHHmmssfff", provider: null);
        return $"{currentDateTimeString}_{Guid.NewGuid()}{fileExtension}";
    }

    private static async Task<string> SaveFile(
        IFormFile file,
        string finalFileDiskPath,
        string folderPrivacyPath,
        string fileUploadFolderName,
        string generatedFileName,
        CancellationToken cancellationToken)
    {
        var fileStream = new FileStream(finalFileDiskPath, FileMode.Create, FileAccess.Write);
        await using (fileStream.ConfigureAwait(false))
        {
            await file.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
        }

        return $"/{folderPrivacyPath}/{fileUploadFolderName}/{generatedFileName}";
    }

    private (
        string finalDiskPath,
        string folderPrivacyPath,
        string fileUploadFolderName,
        string generatedFileName,
        string finalFileSaveDirectory) GenerateFileSaveDetails(IFormFile file, FilePrivacy filePrivacy, string uploadFolder)
    {
        var rootPath = GetRootPath();
        var folderPrivacyPath = filePrivacy == FilePrivacy.Private ? PrivateFolder : PublicFolder;
        var fileUploadFolderName = uploadFolder;

        var finalFileSaveDirectory = Path.Combine(rootPath, folderPrivacyPath, fileUploadFolderName);
        Directory.CreateDirectory(finalFileSaveDirectory);

        var generatedFileName = GetGeneratedFileName(file);
        var finalFileDiskPath = Path.Combine(finalFileSaveDirectory, generatedFileName);

        return (finalFileDiskPath, folderPrivacyPath, fileUploadFolderName, generatedFileName, finalFileSaveDirectory);
    }

    public async Task<FileSaveResult> SaveImage(
        string fileInputName,
        IFormFile file,
        FileHelperOptions fileHelperOptions,
        CancellationToken cancellationToken = default)
    {
        if (file is null)
        {
            return FileSaveResult.Failure($"'{fileInputName}' is empty");
        }

        var fileExtension = Path.GetExtension(file.FileName);
        if (!AllowedImageExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
        {
            return FileSaveResult.Failure($"Invalid image file '{fileInputName}'");
        }

        var (finalDiskPath, folderPrivacyPath, fileUploadFolderName, generatedFileName, finalFileSaveDirectory) = GenerateFileSaveDetails(file, fileHelperOptions.FilePrivacy, fileHelperOptions.FileUploadFolder.ToString());
        var fileSaveURI = await SaveFile(file, finalDiskPath, folderPrivacyPath, fileUploadFolderName, generatedFileName, cancellationToken).ConfigureAwait(false);

        var imageThumbnailWidth = fileHelperOptions.ImageThumbnailWidth;
        var minimumAllowedImageSizeAfterThumbnailWillBeCreated = fileHelperOptions.MinimumAllowedImageSizeAfterThumbnailWillBeCreated;
        var shouldResize = fileHelperOptions.GenerateThumbnail || !fileHelperOptions.GenerateThumbnail && file.Length > minimumAllowedImageSizeAfterThumbnailWillBeCreated;

        if (shouldResize)
        {
            var fileReadStream = file.OpenReadStream();
            await using (fileReadStream.ConfigureAwait(false))
            {
                using var image = new MagickImage(fileReadStream);

                if (imageThumbnailWidth > image.Width)
                {
                    imageThumbnailWidth = image.Width;
                }

                var ratio = (double)image.Width / image.Height;
                int targetHeight = (int)((double)image.Height / image.Width * imageThumbnailWidth);
                var size = new MagickGeometry(imageThumbnailWidth, targetHeight)
                {
                    IgnoreAspectRatio = true,
                };
                image.Resize(size);

                var thumbnailDiskFileName = $"thumbnail_{generatedFileName}";
                var thumbnailFileURI = $"/{folderPrivacyPath}/{fileUploadFolderName}/{thumbnailDiskFileName}";
                await image.WriteAsync(Path.Combine(finalFileSaveDirectory, thumbnailDiskFileName), cancellationToken).ConfigureAwait(false);

                return FileSaveResult.Successful(fileSaveURI, file.FileName, thumbnailFileURI);
            }
        }

        return FileSaveResult.Successful(fileSaveURI, file.FileName);
    }

    public async Task<FileSaveResult> SaveDocument(
        string fileInputName,
        IFormFile file,
        FileHelperOptions fileHelperOptions,
        CancellationToken cancellationToken = default)
    {
        if (file is null)
        {
            return FileSaveResult.Failure($"'{fileInputName}' is empty");
        }

        var fileExtension = Path.GetExtension(file.FileName);
        if (!AllowedDocumentExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
        {
            return FileSaveResult.Failure($"Invalid document file '{fileInputName}'");
        }

        var (finalDiskPath, folderPrivacyPath, fileUploadFolderName, generatedFileName, _) = GenerateFileSaveDetails(file, fileHelperOptions.FilePrivacy, fileHelperOptions.FileUploadFolder.ToString());
        var fileSaveURI = await SaveFile(
            file,
            finalDiskPath,
            folderPrivacyPath,
            fileUploadFolderName,
            generatedFileName,
            cancellationToken).ConfigureAwait(false);

        return FileSaveResult.Successful(fileSaveURI, file.FileName);
    }

    public async Task<FileSaveResult> SaveVideo(
        string fileInputName,
        IFormFile file,
        FileHelperOptions fileHelperOptions,
        CancellationToken cancellationToken = default)
    {
        if (file is null)
        {
            return FileSaveResult.Failure($"'{fileInputName}' is empty");
        }

        var fileExtension = Path.GetExtension(file.FileName);
        if (!AllowedVideoExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
        {
            return FileSaveResult.Failure($"Invalid video file '{fileInputName}'");
        }

        var (finalDiskPath, folderPrivacyPath, fileUploadFolderName, generatedFileName, _) = GenerateFileSaveDetails(file, fileHelperOptions.FilePrivacy, fileHelperOptions.FileUploadFolder.ToString());
        var fileSaveURI = await SaveFile(
            file,
            finalDiskPath,
            folderPrivacyPath,
            fileUploadFolderName,
            generatedFileName,
            cancellationToken).ConfigureAwait(false);

        return FileSaveResult.Successful(fileSaveURI, file.FileName);
    }
}
