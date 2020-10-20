using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index()
        {
            return View();
        }
        //Cargar datos 
        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos()
        {
            EmpleadoModelo modelEmpleado = new EmpleadoModelo();
            var respuesta = modelEmpleado.ConsultarTodos();
            if (respuesta == null)
            {
                return Json(respuesta, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
        }
        //Agregar datos
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult Agregar(etlEmpleado emp)
        {
            try
            {
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                modelEmpleado.GuardarConsulta(emp);
                return Json(emp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }

        [HttpPost]
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Eliminar(long id)
        {
            try
            {
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                modelEmpleado.Eliminar(id);
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Consultar(long id)
        {
            try
            {
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                var respuesta = modelEmpleado.Consultar(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult Actualizar(etlEmpleado emp)
        {
            try
            {
                EmpleadoModelo modelEmpleado = new EmpleadoModelo();
                modelEmpleado.Actualizar(emp);
                return Json(emp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
    }
}