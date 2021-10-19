using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            return View();
        }
    }
}