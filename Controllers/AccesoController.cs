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
                return RedirectToAction("Index", "Solicitud"); 
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
                    long USUARIO = (long)Session["Cedula"];
                    String ACCION = "Inicio de sesión";
                    accesoModelo.GuardarEnBitacora(USUARIO, ACCION, null, null);
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
                long cedula = (long)Session["Cedula"];

                if (usuario.Empleado.Nombre != null){
                    if (usuario.Password != usr.Password) {
                        var MODIFICADO = accesoModelo.ModificarPassword(usr,cedula);

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

        [HttpPost]
        public ActionResult UsuarioID(long ID){
            try{
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.ConsultarUsuarioID(ID);
                if (usuario.Empleado.Nombre != null){
                    return Json("Encontrado", JsonRequestBehavior.AllowGet);
                } else{
                    return Json("No encontrado", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }//FIN DE UsuarioID

        [HttpPost]
        public ActionResult EnviarCorreoRecuperacion(long ID){
            try{
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.ConsultarUsuarioID(ID);

                if (usuario.Empleado.Nombre != null){
                    var ENVIADO = accesoModelo.EnviarCodigoRecuperacion(usuario);

                    if (ENVIADO == true){
                        return Json("Enviado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("No enviado", JsonRequestBehavior.DenyGet);
                    }

                }else{
                    return Json("No enviado", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }//FIN DE EnviarCorreoRecuperacion

        [HttpPost]
        public ActionResult VerificarCodigoRecuperacion(string CODIGO,long CEDULA){
            try{
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.VerificarCodigoRecuperacion(CODIGO,CEDULA);

                if (usuario.Empleado.Cedula != 0){
                    return Json("Coincide", JsonRequestBehavior.AllowGet);
                } else{
                    return Json("No coincide", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }//FIN DE VerificarCodigoRecuperacion

        [HttpPost]
        public ActionResult ModificarContrasenaRecuperacion(long CEDULA,string PASSWORD){
            try{
                AccesoModelo accesoModelo = new AccesoModelo();
                etlUsuario usuario = accesoModelo.ConsultarUsuarioID(CEDULA);
                long cedula = CEDULA;

                if (usuario.Empleado.Nombre != null){
                        usuario.Password = PASSWORD;
                        var MODIFICADO = accesoModelo.ModificarPasswordRecuperacion(usuario,cedula);

                        if (MODIFICADO == true){
                            return Json("Modificado", JsonRequestBehavior.AllowGet);
                        }else{
                            return Json("Error", JsonRequestBehavior.DenyGet);
                        }
                }else{
                    return Json("No encontrado", JsonRequestBehavior.DenyGet);
                }
            }catch (Exception ex){
                return Json("666", JsonRequestBehavior.DenyGet);
            }
        }//FIN DE VerificarCodigoRecuperacion
    }
}