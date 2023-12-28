using Microsoft.AspNetCore.Mvc;

namespace SystemAstraLearn_Kelompok3.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataPelatihan()
        {
            return View();
        }
    }
}
