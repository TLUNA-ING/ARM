﻿using Microsoft.Reporting.WebForms;
using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class EmpleadoController : Controller{
        // GET: Empleado
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index(){
            return View();
        }

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos(){
            EmpleadoModelo modelEmpleado = new EmpleadoModelo();
            var respuesta = modelEmpleado.ConsultarTodos();
            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatos

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult AgregarEmpleado(etlEmpleado emp){
            try{
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                var empleado = modelEmpleado.ConsultarUnEmpleado(emp.Cedula);
                long cedula = (long)Session["Cedula"];

                if (empleado.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else {
                  var AGREGADO =  modelEmpleado.AgregarEmpleado(emp,cedula);
                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }
                }             
            }catch (Exception e) {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE AgregarEmpleado

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarEmpleado(long id){
            try{
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                var respuesta = modelEmpleado.ConsultarUnEmpleadoID(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ConsultarEmpleado

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ModificarEmpleado(etlEmpleado emp){
            try{
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                var empleado = modelEmpleado.ConsultarUnEmpleadoID(emp.Cedula);

                long cedula = (long)Session["Cedula"];
                if (empleado.Nombre != "") {

                    var MODIFICADO = modelEmpleado.ModificarEmpleado(emp,cedula);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    } else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ModificarEmpleado

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarEstado(long id){
            try{
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                var empleado = modelEmpleado.ConsultarUnEmpleadoID(id);

                long cedula = (long)Session["Cedula"];

                if (empleado.Nombre != ""){
                    var MODIFICADO = modelEmpleado.ModificarEstado(empleado,cedula);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    } else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ModificarEstado

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult CargarTipoCedula(){
            try{
                EmpleadoModelo MODEL = new EmpleadoModelo();

                var results = MODEL.ConsultarTipoCedula();
                return Json(results);

            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE CargarTipoCedula

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
            string rutaReporte = "~/Reports/rptEmpleados.rdlc";
            ///construir la ruta física
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<InformacionEmpleados_Result> datosReporte;
            using (var contextoBD = new ARMEntities())
            {
                datosReporte = contextoBD.InformacionEmpleados().ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("EmpleadosDataSet", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;


            return View();



        }//FIN DE REPORT

    }//FIN DE EmpleadoController
}