using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Agro_Mercado.AppMVC.Models;  // verifica que coincida con tu namespace

public class AccountController : Controller
{
    private readonly AgroMercadoSprintContext _context;

    public AccountController(AgroMercadoSprintContext context)
    {
        _context = context;
    }


    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Login(string correo, string password)
    {
        var usuario = _context.Empleados
            .FirstOrDefault(u => u.Correo == correo && u.Password == password);

        if (usuario != null)
        {
            HttpContext.Session.SetString("Usuario", usuario.Nombre);
            HttpContext.Session.SetInt32("RolId", usuario.RolId);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Correo o contraseña incorrectos";
        return View();
    }



    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
}