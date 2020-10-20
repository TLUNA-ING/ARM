using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: USUARIOS
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index()
        {
            return View();
        }


        //Cargar datos 
        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos()
        {
            ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
            var respuesta = modelUsuario.ConsultarTodos();
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
        public ActionResult Agregar(etlUsuario usr){
            try
            {
                ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
                modelUsuario.GuardarConsulta(usr);
                return Json(usr, JsonRequestBehavior.AllowGet);
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
                ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
                modelUsuario.Eliminar(id);
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
                ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
                var respuesta = modelUsuario.Consultar(id);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult Actualizar(etlUsuario usr)
        {
            try
            {
                ListaUsuarioModelo modelUsuario = new ListaUsuarioModelo();
                modelUsuario.Actualizar(usr);
                return Json(usr, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }


        }

        #region "Cambios Tommy"
        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult CARGAR_EMPLEADOS()
        {
            try
            {
                ListaUsuarioModelo MODEL = new ListaUsuarioModelo();
                var results = MODEL.CONSULTAR_EMPLEADOS();
                return Json(results);

            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult CARGAR_ROLES()
        {
            try
            {
                ListaUsuarioModelo MODEL = new ListaUsuarioModelo();
                var resultado = MODEL.CONSULTAR_ROLES();
                return Json(resultado);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion 

    }

}