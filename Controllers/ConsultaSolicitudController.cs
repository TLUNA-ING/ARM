using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class ConsultaSolicitudController : Controller
    {
        // GET: ConsultaSolicitud
        [AutorizarUsuario(rol: "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarDatos()
        {
            ConsultaSolicitudModelo modelSolicitudes = new ConsultaSolicitudModelo();
            var solicitudes = modelSolicitudes.ConsultarTodos();
            if (solicitudes == null)
            {
                return Json(solicitudes, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(solicitudes, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarDatos

        [AutorizarUsuario(rol: "admin")]
        public ActionResult ConsultarSolicitud(long ID)
        {
            try
            {
                ConsultaSolicitudModelo modelSolicitudes = new ConsultaSolicitudModelo();
                var solicitudes = modelSolicitudes.ConsultarUnaSolicitudID(ID);

                return Json(solicitudes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarDepartamento

        //public ActionResult ModificarSolicitud(Solicitudes Solic)
        //{
        //    try
        //    {
        //        DepartamentoModelo modelDepartamento = new DepartamentoModelo();
        //        var departamento = modelDepartamento.ConsultarUnDepartamentoID(depart.ID_Departamento);

        //        if (departamento.Descripcion != "")
        //        {
        //            var MODIFICADO = modelDepartamento.ModificarDepartamento(depart);

        //            if (MODIFICADO == true)
        //            {
        //                return Json("Modificado", JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json("Existe", JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json("XXX", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(e, JsonRequestBehavior.DenyGet);
        //    }
        //}//FIN DE ModificarDepartamento
    }
}