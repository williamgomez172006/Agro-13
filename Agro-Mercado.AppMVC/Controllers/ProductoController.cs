using Microsoft.AspNetCore.Mvc;

namespace Agro_Mercado.AppMVC.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
