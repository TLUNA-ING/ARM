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
    public class ClienteController : Controller{

        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index(){
            return View();
        }//FIN DE Index

        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarDatos(){

            ClienteModelo modelCliente = new ClienteModelo();
            var respuesta = modelCliente.ConsultarTodos();
            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatos


        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ConsultarProvincias(){
            try{
                ClienteModelo modelCliente = new ClienteModelo();

                var results = modelCliente.ConsultarProvincias();
                return Json(results);

            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ConsultarProvincias


        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult AgregarCliente(etlCliente cli) {
            try{
                ClienteModelo modelCliente = new ClienteModelo();
                long cedula = (long)Session["Cedula"];

                var respuesta = modelCliente.ConsultarUnCliente(cli.Nombre);
                if (respuesta.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    var AGREGADO = modelCliente.AgregarCliente(cli,cedula);

                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE AgregarCliente

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarCliente(long ID){
            try{
                ClienteModelo modelCliente = new ClienteModelo();
                var cliente = modelCliente.ConsultarUnClienteID(ID);

                return Json(cliente, JsonRequestBehavior.AllowGet);
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarCliente

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarCliente(etlCliente cli){
            try{
                ClienteModelo modelCliente = new ClienteModelo();
                var tipo = modelCliente.ConsultarUnClienteID(cli.ID_Cliente);
                long cedula = (long)Session["Cedula"];

                if (tipo.Nombre != ""){
                    var MODIFICADO = modelCliente.ModificarCliente(cli,cedula);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("Existe", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ModificarCliente


        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarEstado(long ID){
            try{
                ClienteModelo modelCliente = new ClienteModelo();
                var cliente = modelCliente.ConsultarUnClienteID(ID);
                long cedula = (long)Session["Cedula"];

                if (cliente.Nombre != ""){
                    var MODIFICADO = modelCliente.ModificarEstado(cliente,cedula);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ModificarEstado



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
            string rutaReporte = "~/Reports/rptClientes.rdlc";
            ///construir la ruta física
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<InformacionClientes_Result> datosReporte;
            using (var contextoBD = new ARMEntities())
            {
                datosReporte = contextoBD.InformacionClientes().ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource();
                fuenteDatos.Name = infoFuenteDatos[0];
                fuenteDatos.Value = datosReporte;
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientesDataSet", datosReporte));

                reportViewer.LocalReport.Refresh();
                ViewBag.ReportViewer = reportViewer;


                return View();

            

        }//FIN REPORT

        


    }


}