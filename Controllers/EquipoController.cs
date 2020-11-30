using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class EquipoController : Controller
    {
        // GET: Unidad
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index()
        {
            return View();
        }
        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos()
        {
            EquipoModelo modelUnidad = new EquipoModelo();
            var respuesta = modelUnidad.ConsultarTodos();
            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult AgregarEquipo(etlEquipo equipo){
            try{
                EquipoModelo modelEquipo = new EquipoModelo();

                var respuesta = modelEquipo.ConsultarUnEquipo(equipo.Descripcion);
                if (respuesta.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else{
                    var AGREGADO = modelEquipo.AgregarEquipo(equipo);

                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    } else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }
                }
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE Agregar_Equipo

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarEstado(long id){
            try{
                EquipoModelo modelEquipo = new EquipoModelo();
                var equipo = modelEquipo.ConsultarUnEquipoID(id);
                long cedula = (long)Session["Cedula"];

                if (equipo.Descripcion!=""){
                    var MODIFICADO = modelEquipo.ModificarEstado(equipo,cedula);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ModificarEstado

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarEquipo(long id){
            try{
                EquipoModelo modelEquipo = new EquipoModelo();
                var equipo = modelEquipo.ConsultarUnEquipoID(id);

                return Json(equipo, JsonRequestBehavior.AllowGet);
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarEquipo

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ModificarEquipo(etlEquipo equip){
            try{
                EquipoModelo modelEquipo = new EquipoModelo();
                var equipo = modelEquipo.ConsultarUnEquipoID(equip.ID_Equipo);
                long cedula = (long)Session["Cedula"];

                if (equipo.Descripcion != ""){
                    var MODIFICADO = modelEquipo.ModificarEquipo(equip,cedula);

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
        }//FIN DE ModificarEquipo
    }
}