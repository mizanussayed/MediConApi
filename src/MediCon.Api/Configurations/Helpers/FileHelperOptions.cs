using System.Runtime.InteropServices;

using MediCon.Api.Configurations.Enums;

namespace MediCon.Api.Configurations.Helpers;

[StructLayout(LayoutKind.Auto)]
public record struct FileHelperOptions
{
    public FileUploadFolder FileUploadFolder { get; set; }
    public FilePrivacy FilePrivacy { get; set; }
    public bool GenerateThumbnail { get; set; }
    public int ImageThumbnailWidth { get; set; }
    public int MinimumAllowedImageSizeAfterThumbnailWillBeCreated { get; set; }

    public FileHelperOptions()
    {
    }

    public static FileHelperOptions Image(
        FileUploadFolder fileUploadFolder,
        FilePrivacy filePrivacy,
        bool generateThumbnail,
        int imageThumbnailWidth = 400,
        int minimumAllowedImageSizeAfterThumbnailWillBeCreated = 512)
    {
        return new FileHelperOptions
        {
            FileUploadFolder = fileUploadFolder,
            FilePrivacy = filePrivacy,
            GenerateThumbnail = generateThumbnail,
            ImageThumbnailWidth = imageThumbnailWidth,
            MinimumAllowedImageSizeAfterThumbnailWillBeCreated = minimumAllowedImageSizeAfterThumbnailWillBeCreated,
        };
    }

    public static FileHelperOptions Others(
        FileUploadFolder fileUploadFolder,
        FilePrivacy filePrivacy)
    {
        return new FileHelperOptions
        {
            FileUploadFolder = fileUploadFolder,
            FilePrivacy = filePrivacy,
        };
    }
}
