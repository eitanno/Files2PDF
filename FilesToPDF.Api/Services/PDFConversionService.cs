using iTextSharp.text;
using iTextSharp.text.pdf;


namespace FilesToPDF.Api.Services
{
    public class PDFConversionService
    {
        public void CombinePDFs(string[] inputFiles, string outputFile)
        {
            using (FileStream stream = new FileStream(outputFile, FileMode.Create))
            {
                Document document = new Document();
                PdfCopy pdf = new PdfCopy(document, stream);
                document.Open();
                foreach (string file in inputFiles)
                {
                    using (PdfReader reader = new PdfReader(file))
                    {
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            pdf.AddPage(pdf.GetImportedPage(reader, i));
                        }
                    }
                }
                document.Close();
            }
        }
    }
}
