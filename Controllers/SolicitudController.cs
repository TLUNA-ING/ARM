using Microsoft.Reporting.WebForms;
using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

        public ActionResult Index(){
            etlUsuario usuario = (etlUsuario)Session["User"];
            if (usuario != null) { ViewBag.data = usuario.Empleado.Cedula; }

            return View();
        }//FIN DE Index

        public ActionResult CargarTipoTrabajo(){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarTipoTrabajos();

            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarTipoTrabajo

        public ActionResult CargarEmpleado(){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarEmpleados();

            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarEmpleado

        public ActionResult CargarEmpleadoActual(){
            var respuesta = Session["Cedula"];
            return Json(respuesta);
        }//FIN DE CargarEmpleado


        public ActionResult CargarCliente(etlProvincia provincia) {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarClientes(provincia.ID_Provincia);
            return Json(respuesta);
        }//FIN DE CargarCliente

        public ActionResult CargarDepartamento(etlCliente cliente) {
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarDepartamentos(cliente.ID_Cliente);
            return Json(respuesta);
        }//FIN DE CargarDepartamento

        [HttpPost]
        public ActionResult CargarEquipo(etlDepartamento departamento) {
            try {
                ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
                var respuesta = modelSolicitud.ConsultarEquipos(departamento.ID_Departamento);
                return Json(respuesta);
            } catch (Exception e) {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE CargarEquipo

        public ActionResult CargarProvincia(){
            ConsultaSolicitud modelSolicitud = new ConsultaSolicitud();
            var respuesta = modelSolicitud.ConsultarProvincias();

            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarEquipo

        [HttpPost]
        public ActionResult Agregar(Solicitudes sol){
            try{
                long cedula = (long)Session["Cedula"];
                new ConsultaSolicitud().GuardarConsulta(sol, cedula);
                return Json(sol, JsonRequestBehavior.AllowGet);
            } catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE Agregar

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



        }//FIN DE REPORTE DE SOLICITUDES GENERAL

        public ActionResult EnviaCorreo(etlSolicitud sol)
        {
            try
            {
                ConsultaSolicitudModelo modelConsultaSolicitud = new ConsultaSolicitudModelo();
                var solicitud = modelConsultaSolicitud.ConsultarUnaSolicitudID(sol.ID_Solicitud);

                //var aliasfrom = GetCampo("Nombre Remitente", "Pepito");
                //var emailFrom = GetCampo("Email Remitente", "pepito@mail.com");

                var aliasTo = GetCampo("Nombre Destinatario", sol.nombreMQC);
                var emailTo = GetCampo("Email Destinatario", sol.correoMQC);

                var host = GetCampo("Servidor SMTP", "smtp.gmail.com");
                //var puertoSeguro = GetCampo("El Servidor SMTP usa un puerto Seguro? [S (SÍ)/N (NO)]").ToLower() == "s";

                using (var viewer = new LocalReport())
                {
                    viewer.DataSources.Add(new ReportDataSource("data", sol));
                    viewer.Refresh();
                    // Para que esta línea funcione se debe escoger el archivo DemoReporte.rdlc y en las propiedades de archivo
                    // ajustarlo a "Copiar siempre" en 'Copiar en el directorio de salida'.
                    viewer.ReportPath = "./Reports/DemoReporte.rdlc";
                    var bytes = viewer.Render("PDF");

                    etlSmtp smtp = new etlSmtp(); 
                    var correo = new MailMessage { From = new MailAddress(smtp.Correo, "Bitacoras Capris Medica") };

                    correo.To.Add(new MailAddress(emailTo, aliasTo));
                    correo.Subject = "Reporte como Correo";
                    correo.Attachments.Add(new Attachment(new MemoryStream(bytes), "Reporte.pdf"));

                    correo.Body = "Estimado usuario, se le adjunta el reporte.";

                    SmtpModelo modelSmtp = new SmtpModelo();
                    var SMTP = modelSmtp.ConsultarSmtp();

                    if (SMTP == null)
                    {
                        return Json(SMTP, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        return Json(SMTP, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
            return View();
        }

        private static string GetCampo(string nombreCampo, string sugerencia = "")
        {
            Console.WriteLine("Ingrese el valor para {0}{1}:", nombreCampo, string.IsNullOrEmpty(sugerencia) ? sugerencia : $" Ejm:({sugerencia})");
            return Console.ReadLine();
        }


      

    }





    }

