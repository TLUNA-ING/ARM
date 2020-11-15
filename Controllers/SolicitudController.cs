using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class SolicitudController : Controller{

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Index(){
            etlUsuario usuario = (etlUsuario)Session["User"];
            if (usuario != null) { ViewBag.data = usuario.Empleado.Cedula; }

            return View();
        }//FIN DE Index

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarTipoTrabajo(){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarTipoTrabajos();

            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarTipoTrabajo

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarEmpleado()
        {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarEmpleados();

            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarEmpleado


        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarCliente()
        {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarClientes();

            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarEmpleado


        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarDepartamento()
        {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarDepartamentos();

            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarEmpleado


        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarEquipo()
        {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarEquipos();

            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarEquipo


        ////Agregar datos
        [AutorizarUsuario(rol: "admin,user")]
        [HttpPost]
        public ActionResult Agregar(Solicitudes sol)
        {
            try
            {
                new ConsultaSolicitud().GuardarConsulta(sol);
                return Json(sol, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }

        [AutorizarUsuario(rol: "admin,user")]
        [HttpPost]
        public ActionResult Actualizar(Solicitudes sol)
        {
            try
            {
                new ConsultaSolicitud().Actualizar(sol);
                return Json(sol, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }

        //[HttpPost]
        //[AutorizarUsuario(rol: "admin,user")]
        //public ActionResult Eliminar(string id)
        //{
        //    try
        //    {
        //        new SolicitudModelo().Eliminar(id);
        //        return Json(id, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e, JsonRequestBehavior.DenyGet);
        //    }


        //}
        //[AutorizarUsuario(rol: "admin,user")]
        //public ActionResult Consultar(string id)
        //{
        //    try
        //    {
        //        var respuesta = new SolicitudModelo().Consultar(id);
        //        return Json(respuesta, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e, JsonRequestBehavior.DenyGet);
        //    }


        //}
        //[AutorizarUsuario(rol: "admin,user")]
        //[HttpPost]
        //public ActionResult Actualizar(etlSolicitud sol)
        //{
        //    try
        //    {
        //        new SolicitudModelo().Actualizar(sol);
        //        return Json(sol, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e, JsonRequestBehavior.DenyGet);
        //    }


        //}
    }
}