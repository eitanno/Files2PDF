using Microsoft.Office.Interop.PowerPoint;
using System.IO;


namespace FilesToPDF.Api.Services
{
    public class PowerPointConversionService
    {
        private readonly ILogger<PowerPointConversionService> _logger;

        public PowerPointConversionService(ILogger<PowerPointConversionService> logger)
        {
            _logger = logger;
        }

        public void ConvertPowerPointToPDF(string inputFile, string outputFile)
        {

            Application powerpoint = new Application();
            Presentation presentation = null;
            try
            {
                presentation = powerpoint.Presentations.Open(inputFile);
                presentation.ExportAsFixedFormat(outputFile, PpFixedFormatType.ppFixedFormatTypePDF);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing file: {inputFile}.");
            }
            finally
            {
                if (presentation != null)
                    presentation.Close();
                if (powerpoint != null)
                    powerpoint.Quit();
            }
        }
    }
}
