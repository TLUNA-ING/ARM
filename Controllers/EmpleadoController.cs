using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
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

                if (empleado.Count > 0){
                    return Json("Existe", JsonRequestBehavior.AllowGet);
                }else {
                  var AGREGADO =  modelEmpleado.AgregarEmpleado(emp);
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

                if (empleado.Nombre != ""){
                    var MODIFICADO = modelEmpleado.ModificarEmpleado(emp);

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

                if (empleado.Nombre != ""){
                    var MODIFICADO = modelEmpleado.ModificarEstado(empleado);

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

    }//FIN DE EmpleadoController
}