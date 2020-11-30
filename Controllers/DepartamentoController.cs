using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Area
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index(){
            return View();
        }

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos(){
            DepartamentoModelo modelDepartamento = new DepartamentoModelo();
            var departamentos = modelDepartamento.ConsultarTodos();
            if (departamentos == null){
                return Json(departamentos, JsonRequestBehavior.DenyGet);
            }else{
                return Json(departamentos, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatos

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult AgregarDepartamento(etlDepartamento departamento) {
            try{
                DepartamentoModelo modelDepartamento = new DepartamentoModelo();
                long cedula = (long)Session["Cedula"];

                var respuesta = modelDepartamento.ConsultarUnDepartamento(departamento.Descripcion);
                if (respuesta.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    var AGREGADO = modelDepartamento.AgregarDepartamento(departamento,cedula);

                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE AgregarDepartamento

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarDepartamento(long id){
            try{
                DepartamentoModelo modelDepartamento = new DepartamentoModelo();
                var departamento = modelDepartamento.ConsultarUnDepartamentoID(id);

                return Json(departamento, JsonRequestBehavior.AllowGet);
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarDepartamento

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ModificarDepartamento(etlDepartamento depart){
            try{
                DepartamentoModelo modelDepartamento = new DepartamentoModelo();
                var departamento = modelDepartamento.ConsultarUnDepartamentoID(depart.ID_Departamento);
                long cedula = (long)Session["Cedula"];

                if (departamento.Descripcion != ""){
                    var MODIFICADO = modelDepartamento.ModificarDepartamento(depart,cedula);

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
        }//FIN DE ModificarDepartamento

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarEstado(long id){
            try{
                DepartamentoModelo modelDepartamento = new DepartamentoModelo();
                var departamento = modelDepartamento.ConsultarUnDepartamentoID(id);
                long cedula = (long)Session["Cedula"];

                if (departamento.Descripcion != ""){
                    var MODIFICADO = modelDepartamento.ModificarEstado(departamento,cedula);

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
        }// FIN DE ModificarEstado

    }
}