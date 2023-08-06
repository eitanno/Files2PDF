using Microsoft.Office.Interop.Word;


namespace FilesToPDF.Api.Services
{
    public class WordConversionService
    {

        private readonly ILogger<WordConversionService> _logger;

        public WordConversionService(ILogger<WordConversionService> logger)
        {
            _logger = logger;
        }


        public void ConvertWordToPDF(string inputFile, string outputFile)
        {
            Application word = new Application();
            Document doc = null;
            try
            {
                doc = word.Documents.Open(inputFile);
                doc.ExportAsFixedFormat(outputFile, WdExportFormat.wdExportFormatPDF);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing file: {inputFile}.");
            }
            finally
            {
                if (doc != null)
                    doc.Close(false);
                if (word != null)
                    word.Quit(false);
            }
        }

    }
}
