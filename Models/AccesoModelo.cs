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

    }

}