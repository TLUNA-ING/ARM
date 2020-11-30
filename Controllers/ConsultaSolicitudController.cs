using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class ConsultaSolicitudController : Controller
    {
        // GET: ConsultaSolicitud
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarTipoTrabajo()
        {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarTipoTrabajos();

            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
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


        //[AutorizarUsuario(rol: "admin,user")]
        //public ActionResult CargarCliente()
        //{
        //    ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
        //    var respuesta = modelSolicitud.ConsultarClientes();

        //    if (respuesta == null)
        //    {
        //        return Json(respuesta, JsonRequestBehavior.DenyGet);
        //    }
        //    else
        //    {
        //        return Json(respuesta, JsonRequestBehavior.AllowGet);
        //    }
        //}//FIN DE CargarEmpleado


        //[AutorizarUsuario(rol: "admin,user")]
        //public ActionResult CargarDepartamento()
        //{
        //    ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
        //    var respuesta = modelSolicitud.ConsultarDepartamentos();

        //    if (respuesta == null)
        //    {
        //        return Json(respuesta, JsonRequestBehavior.DenyGet);
        //    }
        //    else
        //    {
        //        return Json(respuesta, JsonRequestBehavior.AllowGet);
        //    }
        //}//FIN DE CargarEmpleado


        //[AutorizarUsuario(rol: "admin,user")]
        //public ActionResult CargarEquipo()
        //{
        //    ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
        //    var respuesta = modelSolicitud.ConsultarEquipos();

        //    if (respuesta == null)
        //    {
        //        return Json(respuesta, JsonRequestBehavior.DenyGet);
        //    }
        //    else
        //    {
        //        return Json(respuesta, JsonRequestBehavior.AllowGet);
        //    }
        //}//FIN DE CargarEquipo

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ConsultarProvincias()
        {
            try
            {
                ConsultaSolicitudModelo modelSolicitud = new ConsultaSolicitudModelo();

                var results = modelSolicitud.ConsultarProvincias();
                return Json(results);

            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ConsultarProvincias

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos()
        {
            ConsultaSolicitudModelo modelSolicitudes = new ConsultaSolicitudModelo();
            var solicitudes = modelSolicitudes.ConsultarTodos();
            if (solicitudes == null)
            {
                return Json(solicitudes, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(solicitudes, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatos

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarSolicitud(long id)
        {
            try
            {
                ConsultaSolicitudModelo modelSolicitudes = new ConsultaSolicitudModelo();
                var solicitudes = modelSolicitudes.ConsultarUnaSolicitudID(id);

                return Json(solicitudes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarSolicitud

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ModificarSolicitud(etlSolicitud sol)
        {
            try
            {
                ConsultaSolicitudModelo modelConsultaSolicitud = new ConsultaSolicitudModelo();
                var solicitud = modelConsultaSolicitud.ConsultarUnaSolicitudID(sol.ID_Solicitud);

                if (solicitud.Empleado.Nombre != "")
                {
                    var MODIFICADO = modelConsultaSolicitud.ModificarSolicitud(sol);

                    if (MODIFICADO == true)
                    {
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ModificarEmpleado

    }
}
