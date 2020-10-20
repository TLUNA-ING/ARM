using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Centro
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index()
        {
            return View();
        }
        //Cargar datos 
        [AutorizarUsuario(rol: "admin,user")]
        public ActionResult CargarDatos()
        {
            ClienteModelo modelCentro = new ClienteModelo();
            var respuesta = modelCentro.ConsultarTodos();
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
        public ActionResult Agregar(etlCliente centro)
        {
            try
            {
                ClienteModelo modelCentro = new ClienteModelo();
                modelCentro.Guardar(centro);
                return Json(centro, JsonRequestBehavior.AllowGet);
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
                ClienteModelo modelCentro = new ClienteModelo();
                modelCentro.Eliminar(id);
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
                ClienteModelo modelCentro = new ClienteModelo();
                var respuesta = modelCentro.Consultar(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult Actualizar(etlCliente centro)
        {
            try
            {
                ClienteModelo modelCentro = new ClienteModelo();
                modelCentro.Actualizar(centro);
                return Json(centro, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
    }
}