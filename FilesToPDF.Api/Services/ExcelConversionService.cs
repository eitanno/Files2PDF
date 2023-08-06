using Microsoft.Office.Interop.Excel;


namespace FilesToPDF.Api.Services
{
    public class ExcelConversionService
    {


        private readonly ILogger<ExcelConversionService> _logger;

        public ExcelConversionService(ILogger<ExcelConversionService> logger)
        {
            _logger = logger;
        }
        public void ConvertExcelToPDF(string inputFile, string outputFile)
        {
            Application excel = new Application();
            Workbook workbook = null;
            try
            {
                workbook = excel.Workbooks.Open(inputFile);
                workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing file: {inputFile}.");
            }
            finally
            {
                if (workbook != null)
                    workbook.Close(false);
                if (excel != null)
                    excel.Quit();
            }
        }
    }
}