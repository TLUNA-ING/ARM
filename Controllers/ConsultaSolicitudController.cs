using Microsoft.Reporting.WebForms;
using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class ConsultaSolicitudController : Controller
    {
        // GET: ConsultaSolicitud
        [AutorizarUsuario(rol: "admin,registrador,modificador")]
        public ActionResult Index()
        {
            return View();
        }


        [AutorizarUsuario(rol: "admin,registrador,modificador")]
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

        [AutorizarUsuario(rol: "admin,registrador,modificador")]
        public ActionResult ConsultarSolicitud(long id){
            try{
                ConsultaSolicitudModelo modelSolicitudes = new ConsultaSolicitudModelo();
                var solicitudes = modelSolicitudes.ConsultarUnaSolicitudID(id);

                return Json(solicitudes, JsonRequestBehavior.AllowGet);
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarSolicitud

        [AutorizarUsuario(rol: "admin,registrador,modificador")]
        [HttpPost]
        public ActionResult ModificarSolicitud(etlSolicitud sol){
            try {
                ConsultaSolicitudModelo modelConsultaSolicitud = new ConsultaSolicitudModelo();
                var solicitud = modelConsultaSolicitud.ConsultarUnaSolicitudID(sol.ID_Solicitud);

                if (solicitud.Empleado.Nombre != "") {
                    var MODIFICADO = modelConsultaSolicitud.ModificarSolicitud(sol);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ModificarEmpleado

        public JsonResult Report2(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<iframe id='ifReporte' width='100%' style='height: 480px' frameborder='0'");
            
            sb.AppendFormat("src='Report1/"+id+"'");
            sb.Append("></iframe>");
            //Retorna el stringBuilder en JSON y se permite todas las peticiones GET

            return this.Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }

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

            reportViewer.LocalReport.EnableExternalImages = true;

            //ReportParameter parm = new ReportParameter();
            //parm = (new ReportParameter("path", @"C:\logo.jpg", true));

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
