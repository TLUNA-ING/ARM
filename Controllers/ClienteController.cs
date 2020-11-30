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

                var respuesta = modelCliente.ConsultarUnCliente(cli.Nombre);
                if (respuesta.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    var AGREGADO = modelCliente.AgregarCliente(cli);

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

                if (tipo.Nombre != ""){
                    var MODIFICADO = modelCliente.ModificarCliente(cli);

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

                if (cliente.Nombre != ""){
                    var MODIFICADO = modelCliente.ModificarEstado(cliente);

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

            List<Clientes> CLIENTES = new List<Clientes>();
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Clientes.Select(x =>
                new etlCliente
                {
                    ID_Cliente = x.clienteId,
                    Provincia = new etlProvincia
                    {
                        ID_Provincia = x.Provincias.provinciaId,
                        Descripcion = x.Provincias.provinciaNombre
                    },
                    Nombre = x.clienteNombre.Trim(),
                    Correo = x.clienteCorreo.Trim(),
                    Estado = x.clienteEstado
                }).ToList();
            }
                ReportDataSource fuenteDatos = new ReportDataSource();
                fuenteDatos.Name = infoFuenteDatos[0];
                fuenteDatos.Value = CLIENTES;
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ClientesDataSet", CLIENTES));

                reportViewer.LocalReport.Refresh();
                ViewBag.ReportViewer = reportViewer;


                return View();

            

        }

    }


}