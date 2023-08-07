namespace MoneyFlow.Models
{
    public class PdfViewModel
    {
        public string PageTitle { get; set; }

        public byte[] PdfFile { get; set; }

        public PdfViewModel()
        {}

        public PdfViewModel(string pageTitle, byte[] pdfFile)
        {
            PageTitle = pageTitle;
            PdfFile = pdfFile;
        }
    }
}