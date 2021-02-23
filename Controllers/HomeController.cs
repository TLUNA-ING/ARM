using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Logout(){
            Session["User"] = null;
            return RedirectToAction("Index", "Acceso");
        }
    }
}