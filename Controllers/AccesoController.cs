using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers{
    public class AccesoController : Controller{
        public ActionResult Index()
        {
            if (Session["User"] == null) { 
                return View(); 
            }else { 
                return RedirectToAction("Index", "Home"); 
            }
        }//FIN DE INDEX

        [HttpPost]
        public ActionResult IniciarSesion(etlUsuario usr){
            try{
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.ValidarAcceso(usr);
                if (usuario.Empleado.Nombre!= null){
                    Session["User"] = usuario;
                    Session["Rol"] = usuario.Rol.Rol;
                    Session["NombreCompletoUsuario"] = usuario.Empleado.Nombre + ' ' + usuario.Empleado.Primer_Apellido + ' ' + usuario.Empleado.Segundo_Apellido; 
                    Session["EmailUsuario"] = usuario.Empleado.Correo;
                    return Json("Encontrado", JsonRequestBehavior.AllowGet);
                }else{
                    return Json("No encontrado", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }
    }
}