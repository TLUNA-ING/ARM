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

        [AutorizarUsuario(rol: "admin,registrador")]
        public ActionResult Index()
        {
            etlUsuario usuario = (etlUsuario)Session["User"];
            if (usuario != null) { ViewBag.data = usuario.Empleado.Cedula; }

            return View();
        }//FIN DE Index


        [AutorizarUsuario(rol: "admin,registrador")]
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

        [AutorizarUsuario(rol: "admin,registrador")]
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


        [AutorizarUsuario(rol: "admin,registrador")]
        public ActionResult CargarCliente(etlProvincia provincia){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarClientes(provincia.ID_Provincia);

            return Json(respuesta);
        }//FIN DE CargarCliente

        [AutorizarUsuario(rol: "admin,registrador")]
        public ActionResult CargarDepartamento(etlCliente cliente){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarDepartamentos(cliente.ID_Cliente);

            return Json(respuesta);
        }//FIN DE CargarDepartamento

        [AutorizarUsuario(rol: "admin,registrador")]
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


        [AutorizarUsuario(rol: "admin,registrador")]
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
        [AutorizarUsuario(rol: "admin,registrador")]
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

        [AutorizarUsuario(rol: "admin,registrador")]
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

        public ActionResult Report()
        {

            var reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                ShowExportControls = true,
                ShowParameterPrompts = true,
                ShowPageNavigationControls = true,
                ShowRefreshButton = true,
                ShowPrintButton = true,
                SizeToReportContent = true,
                AsyncRendering = false,
            };
            string rutaReporte = "~/Reports/rptSolicitudes.rdlc";
            ///construir la ruta física
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<solicitudesPorRangoFechas_Result> datosReporte;
            using (var contextoBD = new ARMEntities())
            {
                datosReporte = contextoBD.solicitudesPorRangoFechas().ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("SolicitudesDataSet", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;


            return View();



        }

    }
}
