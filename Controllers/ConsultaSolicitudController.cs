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
    public class ConsultaSolicitudController : Controller
    {
        // GET: ConsultaSolicitud
        [AutorizarUsuario(rol: "admin,registrador")]
        public ActionResult Index()
        {
            return View();
        }

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

        [AutorizarUsuario(rol: "admin,registrador")]
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

        [AutorizarUsuario(rol: "admin,registrador")]
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

        [AutorizarUsuario(rol: "admin,registrador")]
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

        [AutorizarUsuario(rol: "admin,registrador")]
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

        public ActionResult Report1(int id)
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

            string rutaReporte = "~/Reports/rptDetalleSolicitud.rdlc";
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;

            //var infoFuenteDatos = reportViewer.LocalReport.


            List<SPDetalleSolicitud_Result> datosReporte;
            using (var contextoBD = new ARMEntities())
            {
                datosReporte = contextoBD.SPDetalleSolicitud(id).ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource("DetalleSolicitudDataSet", datosReporte);
            reportViewer.LocalReport.DataSources.Clear();
            //fuenteDatos.Name = infoFuenteDatos[0];
            //fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(fuenteDatos);
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;


            return View();

        }

    }
}
