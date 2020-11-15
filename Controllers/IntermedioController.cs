using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class IntermedioController : Controller
    {

        // GET: Intermedio
        public ActionResult Index(){
            return View();
        }


        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ConsultarClientes(){
            try{
                ClienteModelo modelCliente = new ClienteModelo();

                var results = modelCliente.ConsultarClientesActivos();
                return Json(results);

            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ConsultarProvincias

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ConsultarDepartamentos(){
            try{
                DepartamentoModelo modelDepartamento = new DepartamentoModelo();

                var results = modelDepartamento.ConsultarDepartamentosActivos();
                return Json(results);

            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ConsultarProvincias


        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatosTablas(string TABLA,long ID,string IND_LIGADO){
            IntermedioModelo modelIntermedio = new IntermedioModelo();

            var Datos = modelIntermedio.CargarDatosTablas(TABLA, ID, IND_LIGADO);
            if (Datos == null){
                return Json("", JsonRequestBehavior.DenyGet);
            }else{
                return Json(Datos, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatosDepartamentosNOLigados

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult LigarDepartamento(etlDepartamentoXCliente Intermedio){
            try{
                DepartamentoModelo modelDepartamento = new DepartamentoModelo();
                ClienteModelo modelCliente = new ClienteModelo();
                IntermedioModelo modelIntermedio = new IntermedioModelo();

                var departamento = modelDepartamento.ConsultarUnDepartamentoID(Intermedio.ID_Departamento);
                var cliente = modelCliente.ConsultarUnClienteID(Intermedio.ID_Cliente);
                var ligado = modelIntermedio.ConsultarIDs(Intermedio.ID_Cliente, Intermedio.ID_Departamento);

                if (ligado.ID_Departamento != 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    if (departamento.Descripcion !="" && cliente.Nombre!=""){
                        var LIGADO = modelIntermedio.LigarDepartamentoIntermedio(Intermedio);
                        if (LIGADO == true){
                            return Json("Ligado", JsonRequestBehavior.AllowGet);
                        }else{
                            return Json("XXX", JsonRequestBehavior.AllowGet);
                        }
                    }else{
                        return Json("XXX", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE LigarDepartamento


        public ActionResult DesligarDepartamento(etlDepartamentoXCliente Intermedio){
            try{
                IntermedioModelo modelIntermedio = new IntermedioModelo();

                var ligado = modelIntermedio.ConsultarIDs(Intermedio.ID_Cliente, Intermedio.ID_Departamento);

                if (ligado.ID_Departamento != 0) {

                    var DESLIGADO = modelIntermedio.DesligarDepartamentoIntermedio(Intermedio);

                    if (DESLIGADO == true) {
                        return Json("Desligado", JsonRequestBehavior.AllowGet);
                    } else {
                        return Json("XXX", JsonRequestBehavior.AllowGet);
                    }
                }else{
                    return Json("No Existe", JsonRequestBehavior.AllowGet);
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE LigarDepartamento


    }
}