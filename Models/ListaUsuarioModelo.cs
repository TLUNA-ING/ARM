using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ListaUsuarioModelo
    {

       public List<SelectListItem> CONSULTAR_EMPLEADOS()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = contextoBD.CONSULTAR_EMPLEADOS_NO_USUARIO();
                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.Cedula.ToString(), Text = item.Cedula +" - "+ item.Nombre }).ToList();
                List<SelectListItem> listaEmpleados = new List<SelectListItem>();
                listaEmpleados.AddRange(itemLista);

                return listaEmpleados.ToList();
            }
        }

        public List<SelectListItem> CONSULTAR_ROLES()
        {
            using (var contextoBD = new ARMEntities())
            {

                var roles = (from x in contextoBD.Roles
                                  select x).ToList();

                var itemLista = (from item in roles
                                 select new SelectListItem { Value = item.rolId.ToString(), Text = item.rolNombre.ToString() }).ToList();

                List<SelectListItem> Lista_Roles = new List<SelectListItem>();
                Lista_Roles.AddRange(itemLista);

                return Lista_Roles.ToList();
            }
        }


        public List<etlUsuario> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Usuarios.Select(x =>
                new etlUsuario
                {
                    Empleado = new etlEmpleado
                    {
                        Cedula = x.Empleados.empleadoCedula
                    },
                    Password = x.usuarioContraseña.Trim(),
                    Rol = new etlRol
                    {
                        ID_Rol = x.Roles.rolId,
                        Rol = x.Roles.rolNombre.Trim()
                    }

                }).ToList();
                return respuesta;
            }

        }
        public void GuardarConsulta(etlUsuario usr)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Usuarios item = new Usuarios();

                    item.usuario = usr.Empleado.Cedula;
                    item.usuarioContraseña = usr.Password.Trim();
                    item.rolId = usr.Rol.ID_Rol;


                    contextoBD.Usuarios.Add(item);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Cedula ya existe");
            }

        }
        public void Eliminar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Usuarios usr = (from d in contextoBD.Usuarios
                                    where d.Empleados.empleadoCedula == id
                                    select d).FirstOrDefault();
                    contextoBD.Usuarios.Remove(usr);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlUsuario Consultar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {

                    Usuarios user = contextoBD.Usuarios.Find(id);

                    etlUsuario usuario = new etlUsuario
                    {
                        Empleado = new etlEmpleado {

                            Cedula = user.usuario,
                        },
                        Password = user.usuarioContraseña.Trim(),
                        Rol = new etlRol
                        {
                            ID_Rol = user.Roles.rolId,
                            Rol = user.Roles.rolNombre.Trim(),
                        },

                    };
                    return usuario;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al consultar");
            }

        }
        public void Actualizar(etlUsuario usuario)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    //Usuarios item = contextoBD.Empleado.Join(contextoBD.Usuario,
                    //                 emp => emp.Cedula, usr => usr.Cedula,
                    //                 (emp, usr) => usr).Where(a => a.Cedula.Equals(usuario.Empleado.Cedula)).FirstOrDefault();

                    Usuarios item = contextoBD.Usuarios.Find(usuario.Empleado.Cedula);

                    item.usuarioContraseña = usuario.Password;
                    item.rolId = usuario.Rol.ID_Rol;
                    contextoBD.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al actualizar" + e);
            }

        }
    }
}