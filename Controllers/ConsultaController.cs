using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class ConsultaController : Controller
    {
        // GET: Consulta
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Index()
        {
            return View();
        }
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Consulta()
        {
            SeguimientoModelo modelSeguimiento = new SeguimientoModelo();
            var respuesta = modelSeguimiento.ConsultarTodos();
            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }
    }
}