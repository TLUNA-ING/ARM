﻿using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ListaUsuarioModelo{

        public List<etlUsuario> ConsultarTodos(){
            using (var contextoBD = new ARMEntities()){
                var respuesta = contextoBD.Usuarios.Select(x =>
                new etlUsuario{
                    Empleado = new etlEmpleado{
                        Cedula = x.Empleados.empleadoCedula,
                        Estado = x.Empleados.empleadoEstado
                    },
                    Password= x.usuarioContraseña,
                    Rol = new etlRol{
                        ID_Rol = x.Roles.rolId,
                        Rol = x.Roles.rolNombre.Trim()
                    }
                }).ToList();
                return respuesta;
            }
        }// FIN DE ConsultarTodos

        public List<SelectListItem> CONSULTAR_EMPLEADOS(){
            using (var contextoBD = new ARMEntities()) {
                var result = contextoBD.CONSULTAR_EMPLEADOS_NO_USUARIO();
                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.Cedula.ToString(), Text = item.Cedula +" - "+ item.Nombre }).ToList();
                List<SelectListItem> listaEmpleados = new List<SelectListItem>();
                listaEmpleados.AddRange(itemLista);

                return listaEmpleados.ToList();
            }
        }//FIN DE CONSULTAR_EMPLEADOS

        public List<SelectListItem> CONSULTAR_ROLES(){
            using (var contextoBD = new ARMEntities()){

                var roles = (from x in contextoBD.Roles
                                  select x).ToList();

                var itemLista = (from item in roles
                                 select new SelectListItem { Value = item.rolId.ToString(), Text = item.rolNombre.ToString() }).ToList();

                List<SelectListItem> Lista_Roles = new List<SelectListItem>();
                Lista_Roles.AddRange(itemLista);

                return Lista_Roles.ToList();
            }
        }//FIN DE CONSULTAR_ROLES

        List<Usuarios> USUARIOS = new List<Usuarios>();
        public List<Usuarios> ConsultarUnUsuario(etlUsuario usu){
            try{
                using (var contextoBD = new ARMEntities()){
                    USUARIOS = (from x in contextoBD.Usuarios where x.usuario == usu.Empleado.Cedula select x).ToList();
                    return USUARIOS;
                }

            }catch (Exception e)
            {
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnUsuario

        public bool ActivarUsuario(etlUsuario usr){
            try{
                etlSeguridad seguridad = new etlSeguridad();

                bool ACTIVADO = false;
                using (var contextoBD = new ARMEntities()){

                    Usuarios item = new Usuarios();
                    item.usuario = usr.Empleado.Cedula;
                    item.rolId = usr.Rol.ID_Rol;
                    item.usuarioContraseña = seguridad.Encriptar("ARM123");

                    contextoBD.Usuarios.Add(item);
                    contextoBD.SaveChanges();
                    ACTIVADO = true;
                }
                return ACTIVADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ActivarUsuario
        public etlUsuario ConsultarUnUsuarioID(long ID){
            try{
                etlUsuario usuario = new etlUsuario();
                using (var contextoBD = new ARMEntities()){
                    USUARIOS = (from x in contextoBD.Usuarios where x.usuario == ID select x).ToList();
                    foreach (var USU in USUARIOS){
                        usuario.Empleado.Cedula = USU.usuario;
                        usuario.Empleado.Nombre = USU.Empleados.empleadoNombre;
                        usuario.Empleado.Primer_Apellido = USU.Empleados.empleadoPrimerA;
                        usuario.Empleado.Segundo_Apellido = USU.Empleados.empleadoSegundoA;
                        usuario.Rol.ID_Rol = USU.Roles.rolId;
                    }
                }
                return usuario;
            }catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnUsuarioID

        public bool ModificarUsuario(etlUsuario usr){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var USUARIO = contextoBD.Usuarios.SingleOrDefault(b => b.usuario == usr.Empleado.Cedula);
                    if (USUARIO != null){
                        USUARIO.rolId = usr.Rol.ID_Rol;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarUsuario

    }
}