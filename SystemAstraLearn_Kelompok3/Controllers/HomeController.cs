using SystemAstraLearn_Kelompok3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SystemAstraLearn_Kelompok3.Models;
using System.Diagnostics;

namespace SystemAstraLearn_Kelompok3.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _userRepository;

        public HomeController(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration);
        }

        public IActionResult Login(string username, string password)
        {
            User user = _userRepository.getDataByUsername_Password(username, password);

            if (user.IdPengguna != 0)
            {
                // User found in the database
                // Perform session setup and redirection based on user role
                string serializedModel = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("Identity", serializedModel);
                HttpContext.Session.SetString("Peran", user.HakAkses);
                HttpContext.Session.SetString("User", user.NamaLengkap);
                HttpContext.Session.SetInt32("Id", user.IdPengguna);
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("loggedInUserId", "true");

                if (user.HakAkses == "Peserta")
                {
                    // Set success alert for successful login
                    TempData["LoginSuccessMessage"] = "Login successful. Welcome, " + user.Username + "!";
                    TempData["LoginSuccessType"] = "success";

                    return RedirectToAction("Dashboard", "Peserta");
                }
                else if (user.HakAkses == "Admin")
                {
                    // Set success alert for successful login
                    TempData["LoginSuccessMessage"] = "Login successful. Welcome, " + user.Username + "!";
                    TempData["LoginSuccessType"] = "success";

                    return RedirectToAction("Index", "Admin");
                }
                else if (user.HakAkses == "Pelatih")
                {
                    // Set success alert for successful login
                    TempData["LoginSuccessMessage"] = "Login successful. Welcome, " + user.Username + "!";
                    TempData["LoginSuccessType"] = "success";

                    return RedirectToAction("Index", "Pelatih");
                }
            }
            else
            {
                // User not found in the database
                TempData["AlertMessage"] = "Login failed. Please check your username and password.";
                TempData["AlertType"] = "error";

                // Redirect to the Index action to display the login form with the error message
                return RedirectToAction("Index");
            }

            // Default return statement (added to address the error)
            return View();
        }

        public IActionResult CaraBelajar()
        {
            // Check if user is authenticated
            bool isLoggedIn = User.Identity.IsAuthenticated;

            // Pass the login status to the view
            ViewBag.IsLoggedIn = isLoggedIn;

            return View();
        }

        public IActionResult Index()
        {
            Response.Cookies.Delete(".AspNetCore.Session");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult CaraDapatSertifikat()
        {
            // Check if user is authenticated
            bool isLoggedIn = User.Identity.IsAuthenticated;

            // Pass the login status to the view
            ViewBag.IsLoggedIn = isLoggedIn;

            return View();
        }

        public IActionResult Kelas()
        {
            // Check if user is authenticated
            bool isLoggedIn = User.Identity.IsAuthenticated;

            // Pass the login status to the view
            ViewBag.IsLoggedIn = isLoggedIn;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}