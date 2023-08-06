using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;


namespace FilesToPDF.Api.Services
{
    public class PicConversionService
    {
        private readonly ILogger<PicConversionService> _logger;

        public PicConversionService(ILogger<PicConversionService> logger)
        {
            _logger = logger;
        }
        public void ConvertPicToPDF(string inputFile, string outputFile)
        {
            using (var image = XImage.FromFile(inputFile))
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                // Set the PDF page size to match the image dimensions
                page.Width = image.PixelWidth;
                page.Height = image.PixelHeight;

                gfx.DrawImage(image, 0, 0);

                document.Save(outputFile);
                document.Close();

                _logger.LogInformation("Image converted to PDF successfully!");
            }
        }
    }
}