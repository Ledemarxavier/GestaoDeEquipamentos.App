using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.WebApp.Controllers
{
    public class ChamadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}