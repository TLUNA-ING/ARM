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
                etlSeguridad seguridad = new etlSeguridad();
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.ValidarAcceso(usr);
                if (usuario.Empleado.Nombre!= null){
                    Session["User"] = usuario;
                    Session["Rol"] = usuario.Rol.Rol;
                    Session["Cedula"] = usuario.Empleado.Cedula;
                    Session["NombreCompletoUsuario"] = usuario.Empleado.Nombre + ' ' + usuario.Empleado.Primer_Apellido + ' ' + usuario.Empleado.Segundo_Apellido; 
                    Session["EmailUsuario"] = usuario.Empleado.Correo;

                    if (seguridad.DesEncriptar(usuario.Password)== "ARM123"){
                        Session["CambiarPassword"] = "SI";
                    }else {
                        Session["CambiarPassword"] = "NO";
                    }

                    return Json("Encontrado", JsonRequestBehavior.AllowGet);
                }else{
                    return Json("No encontrado", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }//FIN DE IniciarSesion


        [HttpPost]
        public ActionResult CambiarPassword(etlUsuario usr){
            try{
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.ConsultarUsuarioID(usr.Empleado.Cedula);
                if (usuario.Empleado.Nombre != null){
                    if (usuario.Password != usr.Password) {
                        var MODIFICADO = accesoModelo.ModificarPassword(usr);

                        if (MODIFICADO == true){
                            return Json("Modificado", JsonRequestBehavior.AllowGet);
                        }else{
                            return Json("Contraseña inválida", JsonRequestBehavior.DenyGet);
                        }

                    }else {
                        return Json("Misma contraseña", JsonRequestBehavior.DenyGet);
                    }

                } else{
                    return Json("No encontrado", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }//FIN DE IniciarSesion

    }
}