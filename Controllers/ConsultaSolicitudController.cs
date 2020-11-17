﻿using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
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
        public ActionResult ConsultarSolicitud(long id)
        {
            try
            {
                ConsultaSolicitudModelo modelSolicitudes = new ConsultaSolicitudModelo();
                var solicitudes = modelSolicitudes.ConsultarUnaSolicitudID(id);

                return Json(solicitudes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }// FIN DE ConsultarSolicitud

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ModificarSolicitud(etlSolicitud sol)
        {
            try
            {
                ConsultaSolicitudModelo modelConsultaSolicitud = new ConsultaSolicitudModelo();
                var solicitud = modelConsultaSolicitud.ConsultarUnaSolicitudID(sol.ID_Solicitud);

                if (solicitud.Empleado.Nombre != "")
                {
                    var MODIFICADO = modelConsultaSolicitud.ModificarSolicitud(sol);

                    if (MODIFICADO == true)
                    {
                        return Json("Modificado", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("666", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ModificarEmpleado

    }
}