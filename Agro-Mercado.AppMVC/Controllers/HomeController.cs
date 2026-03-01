using Agro_Mercado.AppMVC.Controllers;
using Agro_Mercado.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agro_Mercado.AppMVC.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var usuario = HttpContext.Session.GetString("Usuario");

            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Usuario = usuario;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
