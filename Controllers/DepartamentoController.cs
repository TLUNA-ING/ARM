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
        public ActionResult Index()
        {
            return View();
        }

        //Cargar datos 
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarDatos()
        {
            DepartamentoModelo modelArea = new DepartamentoModelo();
            var respuesta = modelArea.ConsultarTodos();
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
        public ActionResult Agregar(etlDepartamento area)
        {
            try
            {
                DepartamentoModelo modelArea = new DepartamentoModelo();
                modelArea.GuardarConsulta(area);
                return Json(area, JsonRequestBehavior.AllowGet);
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
                DepartamentoModelo modelArea = new DepartamentoModelo();
                modelArea.Eliminar(id);
                return Json(id, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult Consultar(long id)
        {
            try
            {
                DepartamentoModelo modelArea = new DepartamentoModelo();
                var respuesta = modelArea.Consultar(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult Actualizar(etlDepartamento area)
        {
            try
            {
                DepartamentoModelo modelArea = new DepartamentoModelo();
                modelArea.Actualizar(area);
                return Json(area, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
    }
}