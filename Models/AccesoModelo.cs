using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoProgramacion.Models{
    public class AccesoModelo{

        public etlUsuario ValidarAcceso(etlUsuario usr){
            etlUsuario usuario = new etlUsuario();
            etlSeguridad seguridad = new etlSeguridad();
            string PASSWORD = seguridad.Encriptar(usr.Password);
            using (var contextoBD = new ARMEntities()){
                    Usuarios user = (from d in contextoBD.Usuarios where d.Empleados.empleadoCedula == usr.Empleado.Cedula && d.usuarioContraseña == PASSWORD && d.Empleados.empleadoEstado=="Activo" select d).FirstOrDefault();
                    if (user != null) {
                        usuario.Empleado.Cedula = user.Empleados.empleadoCedula;
                        usuario.Password = user.usuarioContraseña.Trim();
                        usuario.Empleado.Correo = user.Empleados.empleadoCorreo.Trim();
                        usuario.Empleado.Nombre = user.Empleados.empleadoNombre.Trim();
                        usuario.Empleado.Primer_Apellido = user.Empleados.empleadoPrimerA.Trim();
                        usuario.Empleado.Segundo_Apellido = user.Empleados.empleadoSegundoA.Trim();
                        usuario.Empleado.Estado = user.Empleados.empleadoEstado.Trim();

                        usuario.Rol.ID_Rol = user.Roles.rolId;
                        usuario.Rol.Rol = user.Roles.rolNombre.Trim();
                        usuario.Rol.Descripcion = user.Roles.rolDescripcion.Trim();

                    }
                }
            return usuario;
        }//FIN DE ValidarAcceso

        List<Usuarios> USUARIOS = new List<Usuarios>();
        public etlUsuario ConsultarUsuarioID(long ID){
            try{
                etlSeguridad seguridad = new etlSeguridad();
                etlUsuario usuario = new etlUsuario();
                using (var contextoBD = new ARMEntities()){
                    USUARIOS = (from x in contextoBD.Usuarios where x.usuario == ID select x).ToList();
                    foreach (var USU in USUARIOS){
                        usuario.Empleado.Cedula = USU.usuario;
                        usuario.Empleado.Nombre = USU.Empleados.empleadoNombre;
                        usuario.Empleado.Primer_Apellido = USU.Empleados.empleadoPrimerA;
                        usuario.Password = seguridad.DesEncriptar(USU.usuarioContraseña);
                        usuario.Empleado.Segundo_Apellido = USU.Empleados.empleadoSegundoA;
                        usuario.Empleado.Correo = USU.Empleados.empleadoCorreo;
                        usuario.Rol.ID_Rol = USU.Roles.rolId;
                    }
                }
                return usuario;
            }catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnUsuarioID

        public bool ModificarPassword(etlUsuario usr){
            try {
                etlSeguridad seguridad = new etlSeguridad();
                bool MODIFICADO = false;
                var PasswordEncriptada = seguridad.Encriptar(usr.PasswordActual);
                using (var contextoBD = new ARMEntities()){
                    var USUARIO = contextoBD.Usuarios.SingleOrDefault(b => b.usuario == usr.Empleado.Cedula && b.usuarioContraseña == PasswordEncriptada);
                    if (USUARIO != null){

                        USUARIO.usuarioContraseña = seguridad.Encriptar(usr.Password);
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;
            } catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEmpleado

        private static string _numbers = "ABCDEFGHIJKLMNOPQRSTVWXYZ0123456789";
        Random random = new Random();

        public string RandomString(int tamano){
            StringBuilder builder = new StringBuilder(tamano);
            string numberAsString;

            for (var i = 0; i < tamano; i++){
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }
            numberAsString = builder.ToString();
            return numberAsString;
        }//FIN DE RandomString

        public bool EnviarCodigoRecuperacion(etlUsuario usr){
            try {
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities()){
                    var CODIGO = RandomString(4);
                    var CEDULA = usr.Empleado.Cedula.ToString();
                    var result = contextoBD.INSERTAR_CODIGO_USUARIO_RECUPERACION(CEDULA, CODIGO);

                    string SALTO = "</br>";
                    string MENSAJE = "<p>Estimado "+ usr.Empleado.Nombre+" "+usr.Empleado.Primer_Apellido+" "+usr.Empleado.Segundo_Apellido + ".</p>";
                    MENSAJE += SALTO;
                    MENSAJE += "<p>Debido a la solicitud realizada para la recuperación de su contraseña por este medio enviamos el código verificador.</p>";
                    MENSAJE += SALTO;
                    MENSAJE += "<p>Que corresponde a:</p>";
                    MENSAJE += SALTO;
                    MENSAJE += "<p><strong>"+ CODIGO + "</strong></p>";
                    MENSAJE += SALTO + SALTO + SALTO;
                    MENSAJE += "<p>Si usted no solicitó este código de recuperación por favor pongase en contacto con su empresa.</p>";
                    MENSAJE += SALTO + SALTO;
                    MENSAJE += "<p><strong>Esto corresponde a un email automático por favor no responderlo.</strong></p>";

                    SmtpModelo modelSmtp = new SmtpModelo();
                    AGREGADO = modelSmtp.EnviarCorreo(usr.Empleado.Correo,"Recuperación de contraseña", MENSAJE);

                }
                return AGREGADO;
            } catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEmpleado

        List<CodigoRecuperacion> RECUPERACION = new List<CodigoRecuperacion>();

        public etlUsuario VerificarCodigoRecuperacion(string CODIGO,long USUARIO){
            try{
                etlSeguridad seguridad = new etlSeguridad();
                etlUsuario usuario = new etlUsuario();

                using (var contextoBD = new ARMEntities()){
                    RECUPERACION = (from x in contextoBD.CodigoRecuperacion where x.usuario == USUARIO && x.codigo == CODIGO select x).ToList();
                    foreach (var USU in RECUPERACION){
                        usuario.Empleado.Cedula = USU.usuario;
                    }
                }
                return usuario;
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnUsuarioID

        public bool ModificarPasswordRecuperacion(etlUsuario usr){
            try{
                etlSeguridad seguridad = new etlSeguridad();
                bool MODIFICADO = false;

                using (var contextoBD = new ARMEntities()){
                    var USUARIO = contextoBD.Usuarios.SingleOrDefault(b => b.usuario == usr.Empleado.Cedula);
                    if (USUARIO != null){
                        USUARIO.usuarioContraseña = seguridad.Encriptar(usr.Password);
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;
            }
            catch (Exception e){
                return false;
            }
        }//FIN DE ModificarPasswordRecuperacion

    }

}