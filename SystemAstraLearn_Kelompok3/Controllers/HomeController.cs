﻿using SystemAstraLearn_Kelompok3.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SystemAstraLearn_Kelompok3.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration);
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [Route("Login/{username}")]
        public IActionResult Login(string username)
        {
            User user = _userRepository.getDataByUsername_Password(username);

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

                if (user.HakAkses == "peserta")
                {
                    // Set success alert for successful login
                    TempData["LoginSuccessMessage"] = "Login successful. Welcome, " + user.Username + "!";
                    TempData["LoginSuccessType"] = "success";

                    return RedirectToAction("Dashboard", "Peserta");
                }
                else if (user.HakAkses == "admin")
                {
                    // Set success alert for successful login
                    TempData["LoginSuccessMessage"] = "Login successful. Welcome, " + user.Username + "!";
                    TempData["LoginSuccessType"] = "success";

                    return RedirectToAction("Index", "Admin");
                }
                else if (user.HakAkses == "pelatih")
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

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Query SQL untuk mengambil klasifikasi dari tabel
                string sqlQuery = "SELECT nama_klasifikasi FROM tb_klasifikasi_pelatihan where status = 1";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Eksekusi query
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ambil nilai NamaKlasifikasi dari hasil query
                            string klasifikasiName = reader["nama_klasifikasi"].ToString();

                            // Pass the klasifikasi name to the view
                            ViewBag.KlasifikasiName = klasifikasiName;
                        }
                        else
                        {
                            // Handle the case where no klasifikasi is found
                            ViewBag.KlasifikasiName = "No Klasifikasi Found";
                        }
                    }
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}