using Microsoft.AspNetCore.Mvc;

namespace SystemAstraLearn_Kelompok3.Controllers
{
    public class DataPelatihanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        [Route("Section/{nama}/{id}")]
        public IActionResult Section(string nama, int id)
        {
            ViewBag.idPelatihan = id;
            ViewBag.namaPelatihan = nama;
            return View();
        }
        [Route("Exercise/{nama}/{id}/{id2}")]
        public IActionResult Exercise(string nama, int id, int id2)
        {
            ViewBag.idPelatihan = id2;
            ViewBag.idSection = id;
            ViewBag.namaSection = nama;
            return View();
        }
        [Route("Exam/{nama}/{id}/{id2}")]
        public IActionResult Exam(string nama, int id, int id2)
        {
            ViewBag.idPelatihan = id;
            ViewBag.namaPelatihan = nama;
            ViewBag.nilaiMinimum = id2;
            return View();
        }
        [Route("SectionView/{id}/{id1}")]
        public IActionResult SectionView(int id, int id1)
        {
            ViewBag.idSection = id;
            ViewBag.idPelatihan = id1;
            return View();
        }
    }
}
