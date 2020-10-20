using ProyectoProgramacion.ETL;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class UsuarioModelo
    {
        public etlUsuario Autenticar(etlUsuario Usuario)
        {
            etlUsuario autenticado = null;
            using (var contextoBD = new ARMEntities())
            {
                Usuarios user = (from d in contextoBD.Usuarios
                                 where d.Empleados.empleadoCedula == Usuario.Empleado.Cedula &&
                                 d.usuarioContraseña == Usuario.Password.Trim()
                                 select d).FirstOrDefault();
                if (user != null)
                {
                    autenticado = new etlUsuario
                    {
                        Password = user.usuarioContraseña.Trim(),
                        Empleado = new etlEmpleado
                        {
                            Cedula = user.Empleados.empleadoCedula,
                            Correo = user.Empleados.empleadoCorreo.Trim(),
                            Nombre = user.Empleados.empleadoNombre.Trim(),
                            Primer_Apellido = user.Empleados.empleadoPrimerA.Trim(),
                            Segundo_Apellido = user.Empleados.empleadoSegundoA.Trim()
                        },
                        Rol = new etlRol
                        {
                            ID_Rol = user.Roles.rolId,
                            Rol = user.Roles.rolNombre.Trim(),
                            Descripcion = user.Roles.rolDescripcion.Trim()

                        }


                    };
                }
            }
            return autenticado;
        }

    }
}