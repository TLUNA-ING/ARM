using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class UsuarioController : Controller{

        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index(){
            return View();
        }//FIN DE Index

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos(){
            ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
            var respuesta = modelUsuario.ConsultarTodos();
            if (respuesta == null){
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }else{
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE 
        //Agregar datos CargarDatos

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ActivarUsuario(etlUsuario usr){
            try{
                ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
                var respuesta = modelUsuario.ConsultarUnUsuario(usr);

                if (respuesta.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    var ACTIVADO = modelUsuario.ActivarUsuario(usr);
                    if (ACTIVADO == true){
                        return Json("Activado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("XXX", JsonRequestBehavior.AllowGet);
                    }
                }
            } catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ActivarUsuario

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult CARGAR_EMPLEADOS(){
            try{
                ListaUsuarioModelo MODEL = new ListaUsuarioModelo();
                var results = MODEL.CONSULTAR_EMPLEADOS();
                return Json(results);

            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE CARGAR_EMPLEADOS

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult CARGAR_ROLES(){
            try{
                ListaUsuarioModelo MODEL = new ListaUsuarioModelo();
                var resultado = MODEL.CONSULTAR_ROLES();
                return Json(resultado);
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE CARGAR_ROLES

    }

}