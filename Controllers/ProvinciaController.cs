using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class ProvinciaController : Controller
    {
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult CargarDatos()
        {
            try
            {
                ProvinciaModelo MODEL = new ProvinciaModelo();
                var resultado = MODEL.ConsultarTodos();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }
    }
}