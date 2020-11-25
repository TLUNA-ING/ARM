using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }

}