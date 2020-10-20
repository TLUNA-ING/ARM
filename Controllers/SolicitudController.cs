using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Solicitud
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Index()
        {
            etlUsuario usuario = (etlUsuario)Session["User"];
            if (usuario != null) { ViewBag.data = usuario.Empleado.Cedula; }

            return View();
        }
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarDatos()
        {
            var respuesta = new SolicitudModelo().ConsultarTodos();
            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }


        //Agregar datos
        [AutorizarUsuario(rol: "admin,user")]
        [HttpPost]
        public ActionResult Agregar(etlSolicitud sol)
        {
            try
            {
                new SolicitudModelo().GuardarConsulta(sol);
                return Json(sol, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }

        [HttpPost]
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Eliminar(string id)
        {
            try
            {
                new SolicitudModelo().Eliminar(id);
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Consultar(string id)
        {
            try
            {
                var respuesta = new SolicitudModelo().Consultar(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin,user")]
        [HttpPost]
        public ActionResult Actualizar(etlSolicitud sol)
        {
            try
            {
                new SolicitudModelo().Actualizar(sol);
                return Json(sol, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
    }
}