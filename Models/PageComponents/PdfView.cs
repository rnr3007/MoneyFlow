namespace MoneyFlow.Models
{
    public class PdfView
    {
        public string PageTitle { get; set; }

        public byte[] PdfFile { get; set; }

        public PdfView()
        {}

        public PdfView(string pageTitle, byte[] pdfFile)
        {
            PageTitle = pageTitle;
            PdfFile = pdfFile;
        }
    }
}