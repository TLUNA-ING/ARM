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
    public class TipoTrabajoController : Controller
    {
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index(){
            return View();
        }//FIN DE Index

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos(){
            TipoTrabajoModelo modelTipo = new TipoTrabajoModelo();
            var tipos = modelTipo.ConsultarTodos();
            if (tipos == null){
                return Json(tipos, JsonRequestBehavior.DenyGet);
            }else{
                return Json(tipos, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatos

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult AgregarTipoTrabajo(etlTipoTrabajo tipo){
            try {
                TipoTrabajoModelo modelTipo = new TipoTrabajoModelo();

                var respuesta = modelTipo.ConsultarUnTipoTrabajo(tipo.Descripcion);
                if (respuesta.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    var AGREGADO = modelTipo.AgregarTipoTrabajo(tipo);

                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE AgregarTipoTrabajo

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarTipoTrabajo(long id){
            try{
                TipoTrabajoModelo modelTipo = new TipoTrabajoModelo();
                var tipo = modelTipo.ConsultarUnTipoTrabajoID(id);

                return Json(tipo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarEquipo

        public ActionResult ModificarTipoTrabajo(etlTipoTrabajo tip){
            try{
                TipoTrabajoModelo modelTipo = new TipoTrabajoModelo();
                var tipo = modelTipo.ConsultarUnTipoTrabajoID(tip.ID_TipoTrabajo);
                long cedula = (long)Session["Cedula"];

                if (tipo.Descripcion != ""){
                    var MODIFICADO = modelTipo.ModificarTipoTrabajo(tip,cedula);

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
        }//FIN DE ModificarTipoTrabajo

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarEstado(long id){
            try{
                TipoTrabajoModelo modelTipo = new TipoTrabajoModelo();
                var tipo = modelTipo.ConsultarUnTipoTrabajoID(id);
                long cedula = (long)Session["Cedula"];

                if (tipo.Descripcion != ""){
                    var MODIFICADO = modelTipo.ModificarEstado(tipo,cedula);

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
    }
}