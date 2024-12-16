using System;
using System.IO;

using DinkToPdf;
using DinkToPdf.Contracts;

public class PdfGenerator
{
    private readonly IConverter _converter;

    public PdfGenerator(IConverter converter)
    {
        _converter = converter;
    }

    public void GeneratePdf(string htmlContent, string outputFilePath)
    {
        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
            Objects = {
                new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Generated using DinkToPdf" }
                }
            }
        };

        byte[] pdf = _converter.Convert(pdfDocument);

        File.WriteAllBytes(outputFilePath, pdf);
        //Console.WriteLine($"PDF saved to {outputFilePath}");
    }
}
