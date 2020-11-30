using Microsoft.Reporting.WebForms;
using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class SolicitudController : Controller
    {

        private etlSolicitud objSolicitud;

        public SolicitudController()
        {

            objSolicitud = new etlSolicitud();
        }

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Index()
        {
            etlUsuario usuario = (etlUsuario)Session["User"];
            if (usuario != null) { ViewBag.data = usuario.Empleado.Cedula; }

            return View();
        }//FIN DE Index


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


        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarCliente(etlProvincia provincia){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarClientes(provincia.ID_Provincia);

            return Json(respuesta);
        }//FIN DE CargarCliente

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarDepartamento(etlCliente cliente){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarDepartamentos(cliente.ID_Cliente);

            return Json(respuesta);
        }//FIN DE CargarDepartamento

        [AutorizarUsuario(rol: "admin,user")]
        [HttpPost]
        public ActionResult CargarEquipo(etlDepartamento departamento){
            try{
                ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
                var respuesta = modelSolicitud.ConsultarEquipos(departamento.ID_Departamento);

                return Json(respuesta);

            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE CargarEquipo


        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarProvincia()
        {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarProvincias();

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
                long cedula = (long)Session["Cedula"];
                new ConsultaSolicitud().GuardarConsulta(sol,cedula);
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
                long cedula = (long)Session["Cedula"];
                new ConsultaSolicitud().Actualizar(sol,cedula);
                return Json(sol, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }

        }

    }
}
