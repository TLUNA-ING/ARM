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
            try
            {
                EquipoModelo modelEquipo = new EquipoModelo();

                var respuesta = modelEquipo.ConsultarUnEquipo(equipo.Descripcion);
                if (respuesta.Count > 0)
                {
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var AGREGADO = modelEquipo.GuardarConsulta(equipo);

                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    }
                    else{
                        return Json("XXX", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE Agregar_Equipo

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult ModificarEstado(long id){
            try{
                EquipoModelo modelEquipo = new EquipoModelo();
                var equipo = modelEquipo.ConsultarUnEquipoID(id);

                if (equipo.Descripcion!=""){
                    var MODIFICADO = modelEquipo.ModificarEstado(equipo);

                    if (MODIFICADO == true){
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("XXX", JsonRequestBehavior.AllowGet);
                    }

                }else{
                    return Json("XXX", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ModificarEstado

        [AutorizarUsuario(rol: "admin")]
        public ActionResult Consultar(long id)
        {
            try
            {
                EquipoModelo modelUnidad = new EquipoModelo();
                var respuesta = modelUnidad.Consultar(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult Actualizar(etlEquipo uni)
        {
            try
            {
                EquipoModelo modelUnidad = new EquipoModelo();
                modelUnidad.Actualizar(uni);
                return Json(uni, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
    }
}