using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using System.IO;
using System.IO.Compression;
using System.Reflection.Metadata;
using DocumentITextSharp = iTextSharp.text.Document;

namespace SystemAstraLearn_Kelompok3.Controllers
{
    public class SertifikatController : Controller
    {
        private readonly ILogger<SertifikatController> _logger;
        BaseFont HeaderFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont ContentFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont TableFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        public SertifikatController(ILogger<SertifikatController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Sertifikat/{nama}/{nama_pelatihan}/{tanggal}")]
        public IActionResult GeneratePdf(string nama, string nama_pelatihan, string tanggal)
        {
            // Define a memory stream to store the PDF
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create a new document with A4 size and landscape orientation
                DocumentITextSharp doc = new DocumentITextSharp(PageSize.A4.Rotate());

                // Create a PdfWriter instance
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

                // Open the document for writing
                doc.Open();

                // Get the PdfContentByte object from the writer
                PdfContentByte cb = writer.DirectContent;

                // Adjust the scale and position of the image to fill the entire landscape page
                var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "SERTIFIKAT.png");

                if (System.IO.File.Exists(imagePath2))
                {
                    iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(imagePath2);
                    img2.ScaleAbsolute(PageSize.A4.Rotate().Width, PageSize.A4.Rotate().Height); // Adjust width and height to match the page
                    img2.SetAbsolutePosition(0, 0); // Set the position to the bottom-left corner
                    cb.AddImage(img2);
                    content(cb, nama, nama_pelatihan, tanggal);
                }
                else
                {
                    // Handle the case where the image file doesn't exist
                    throw new FileNotFoundException("Image file not found: " + imagePath2);
                }

                // Close the document
                doc.Close();

                // Return the PDF as a FileContentResult to open in a new tab
                return File(memoryStream.ToArray(), "application/pdf");
            }
        }

        public void content(PdfContentByte cb, string nama, string pelatihan, string tanggal)
        {
            // Title Text
            cb.BeginText();
            cb.SetFontAndSize(HeaderFont, 36);

            // Set color and display "Nama Peserta", string n
            cb.SetColorFill(new BaseColor(System.Drawing.ColorTranslator.FromHtml("#53AFEF")));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, nama, 125, 345, 0);

            // Reset color for subsequent text
            cb.SetColorFill(new BaseColor(System.Drawing.ColorTranslator.FromHtml("#0D61A8")));

            // Continue displaying other text
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, pelatihan, 125, 255, 0);
            cb.SetFontAndSize(ContentFont, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "..../AL/NMPLTHN/THNPLTHN", 250, 212, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, tanggal, 607, 165, 0);


            cb.EndText();
        }
    }
}

