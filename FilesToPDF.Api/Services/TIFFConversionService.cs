using ImageMagick;


namespace FilesToPDF.Api.Services
{
    public class TIFFConversionService
    {
        private readonly ILogger<TIFFConversionService> _logger;

        public TIFFConversionService(ILogger<TIFFConversionService> logger)
        {
            _logger = logger;
        }
        public void ConvertPicToPDF(string inputFile, string outputFile)
        {
            using (var tiffImages = new MagickImageCollection())
            {
                // Load the TIFF image
                tiffImages.Read(inputFile);

                // Create a new PDF document
                using (var pdf = new MagickImageCollection())
                {
                    foreach (var tiffImage in tiffImages)
                    {
                        // Convert each TIFF image frame to PDF
                        var pdfImage = new MagickImage(tiffImage);
                        pdf.Add(pdfImage);
                    }

                    // Write the PDF document
                    pdf.Write(outputFile);
                }
            }
            _logger.LogInformation("Tiff converted to PDF successfully!");
        }
    }
}