using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Configuration;
using FilesToPDF.Api.Services;

namespace FilesToPDF.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesToPDFController : ControllerBase
{

    private readonly ILogger<FilesToPDFController> _logger;
    private readonly WordConversionService _wordConversionService;
    private readonly ExcelConversionService _excelConversionService;
    private readonly PowerPointConversionService _powerPointConversionService;
    private readonly TIFFConversionService _tiffConversionService;
    private readonly PicConversionService _picConversionService;

    public FilesToPDFController(ILogger<FilesToPDFController> logger,
    WordConversionService wordConversionService, ExcelConversionService excelConversionService,
    PowerPointConversionService powerPointConversionService, TIFFConversionService tiffConversionService,
    PicConversionService picConversionService)
    {
        _logger = logger;
        _wordConversionService = wordConversionService;
        _excelConversionService = excelConversionService;
        _powerPointConversionService = powerPointConversionService;
        _tiffConversionService = tiffConversionService;
        _picConversionService = picConversionService;
    }

    [HttpGet(Name = "GetPDFFromFiles")]
    public string Get()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string inputPath = config["InputPath"] ?? string.Empty;
        string outputPath = config["OutputPath"] ?? string.Empty;
        string outputFileConfigName = config["OutputFileName"] ?? string.Empty;

        if (string.IsNullOrEmpty(inputPath))
        {
            _logger.LogWarning("Please add InputPath to config file");
            return "Please add InputPath to config file";
        }

        if (string.IsNullOrEmpty(outputPath))
        {
            _logger.LogWarning("Please add OutputPath to config file");
            return "Please add OutputPath to config file";
        }

        if (string.IsNullOrEmpty(outputFileConfigName))
        {
            _logger.LogWarning("Please add OutputFileName to config file");
            return "Please add OutputFileName to config file";
        }


        string[] files = Directory.GetFiles(inputPath);
        List<string> pdfFileNames = new List<string>();

        foreach (string file in files)
        {
            string fileType = Path.GetExtension(file);
            string fileName = Path.GetFileNameWithoutExtension(file);
            string outputFileName = outputPath + "\\" + fileName + ".pdf";

            if (fileName.Contains("~"))
                continue;
            _logger.LogInformation($"fileName: {fileName}, file type: {fileType} will be converted");
            switch (fileType)
            {
                case ".docx":
                    _wordConversionService.ConvertWordToPDF(file, outputFileName);
                    pdfFileNames.Add(outputFileName);
                    break;
                case ".xls":
                    _excelConversionService.ConvertExcelToPDF(file, outputFileName);
                    pdfFileNames.Add(outputFileName);
                    break;
                case ".pptx":
                    _powerPointConversionService.ConvertPowerPointToPDF(file, outputFileName);
                    pdfFileNames.Add(outputFileName);
                    break;
                case ".pdf":
                    outputFileName = file;
                    pdfFileNames.Add(outputFileName);
                    break;
                case ".tif":
                case ".tiff":
                    _tiffConversionService.ConvertPicToPDF(file, outputFileName);
                    pdfFileNames.Add(outputFileName);
                    break;
                case ".jpeg":
                case ".jpg":
                case ".png":
                    _picConversionService.ConvertPicToPDF(file, outputFileName);
                    pdfFileNames.Add(outputFileName);
                    break;
                // Add more cases for other file types you want to handle
                default:
                    _logger.LogWarning($"Unsupported file type: {fileType}");
                    break;
            }
        }
        string outputFile = outputPath + "\\" + getFileName(outputFileConfigName) + ".pdf";
        PDFConversionService pdfConversionService = new PDFConversionService();
        pdfConversionService.CombinePDFs(pdfFileNames.ToArray(), outputFile);
        _logger.LogInformation($"File processed successfully : {outputFile}");
        return outputFile;
    }

    private string getFileName(string outputFileConfigName)
    {
        // Get the current date and time
        DateTime currentDate = DateTime.Now;

        // Format the date as a string
        string formattedDate = currentDate.ToString("yyyyMMdd_HHmmss");

        // Append the formatted date to the original file name
        string newFileName = $"{outputFileConfigName}_{formattedDate}";

        // Use the new file name for further processing or saving

        return newFileName;
    }

}
