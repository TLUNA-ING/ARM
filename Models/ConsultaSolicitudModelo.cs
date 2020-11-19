using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ConsultaSolicitudModelo
    {
        public List<etlSolicitud> ConsultarTodos()
        {

            using (var contextoBD = new ARMEntities())
            {
                var solicitudes = contextoBD.Solicitudes.Select(x =>
                new etlSolicitud
                {
                    ID_Solicitud = x.solicitudId,
                    Provincia = new etlProvincia{
                    Descripcion = x.Provincias.provinciaNombre
                        //ID_Cliente = (int)x.Clientes.clienteId

                    },
                    Cliente = new etlCliente { 
                    Nombre = x.Clientes.clienteNombre
                    //ID_Cliente = (int)x.Clientes.clienteId
                                        
                    },
                    Empleado = new etlEmpleado { 
                    Nombre = x.Empleados.empleadoNombre
                    //Cedula = (int)x.empleadoCedula
                    },
                    TipoTrabajo = new etlTipoTrabajo { 
                    Descripcion = x.TipoTrabajo.tipoTrabajoNombre
                    //ID_TipoTrabajo = (int)x.tipoTrabajoId
                    },
                    Departamento = new etlDepartamento { 
                    Descripcion = x.Departamentos.deparatamentoNombre
                    //ID_Departamento = (int)x.departamentoId
                    },
                    Equipo = new etlEquipo {
                    Descripcion = x.Equipos.equipoNombre
                     //ID_Equipo = (int)x.equipoId   
                    },
                    Fecha_Reporte = x.fechaReporte,
                    horaEntrada = x.horaEntrada,
                    horaSalida = x.horaSalida,
                    tipoHora = x.tipoHora,
                    cantidadHoras = (int)x.cantidadHoras,
                    solicitudMotivo = x.solicitudMotivo,
                    motivoDetalle = x.motivoDetalle,
                    solicitudRepuestos = x.solicitudRepuestos,
                    equipoDetenido = (int)x.equipoDetenido

                }).ToList();
                return solicitudes;
            }
        }//FIN DE ConsultarTodos

        public List<SelectListItem> ConsultarClientes()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from cliente in contextoBD.Clientes select cliente).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.clienteId.ToString(), Text = item.clienteNombre }).ToList();

                List<SelectListItem> listaCliente = new List<SelectListItem>();
                listaCliente.AddRange(itemLista);

                return listaCliente.ToList();
            }
        }//FIN DE ConsultarClientes

        public List<SelectListItem> ConsultarProvincias()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from provincia in contextoBD.Provincias select provincia).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.provinciaId.ToString(), Text = item.provinciaNombre }).ToList();

                List<SelectListItem> listaProvincia = new List<SelectListItem>();
                listaProvincia.AddRange(itemLista);

                return listaProvincia.ToList();
            }
        }//FIN DE ConsultarClientes


        public List<SelectListItem> ConsultarDepartamentos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from departamento in contextoBD.Departamentos select departamento).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.departamentoId.ToString(), Text = item.deparatamentoNombre }).ToList();

                List<SelectListItem> listaDepartamento = new List<SelectListItem>();
                listaDepartamento.AddRange(itemLista);

                return listaDepartamento.ToList();
            }
        }//FIN DE ConsultarDepartamentos
        public List<SelectListItem> ConsultarTipoTrabajos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from tipo in contextoBD.TipoTrabajo select tipo).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.tipoTrabajoId.ToString(), Text = item.tipoTrabajoNombre }).ToList();

                List<SelectListItem> listaTipoTrabjo = new List<SelectListItem>();
                listaTipoTrabjo.AddRange(itemLista);

                return listaTipoTrabjo.ToList();
            }
        }//FIN DE ConsultarTipoTrabajos


        public List<SelectListItem> ConsultarEmpleados()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from empleado in contextoBD.Empleados select empleado).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.empleadoCedula.ToString(), Text = item.empleadoNombre }).ToList();

                List<SelectListItem> listaEmpleado = new List<SelectListItem>();
                listaEmpleado.AddRange(itemLista);

                return listaEmpleado.ToList();
            }
        }//FIN DE ConsultarEmpleados


        public List<SelectListItem> ConsultarEquipos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from equipo in contextoBD.Equipos select equipo).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.equipoId.ToString(), Text = item.equipoNombre }).ToList();

                List<SelectListItem> listaEquipo = new List<SelectListItem>();
                listaEquipo.AddRange(itemLista);

                return listaEquipo.ToList();
            }
        }//FIN DE ConsultarEquipos

        public etlSolicitud ConsultarUnaSolicitudID(long ID)
        {
            try
            {
                etlSolicitud solicitudes = new etlSolicitud();
                using (var contextoBD = new ARMEntities())
                {
                   SOLICITUDES  = (from x in contextoBD.Solicitudes where x.solicitudId == ID select x).ToList();
                    foreach (var SOL in SOLICITUDES)
                    {
                        solicitudes.ID_Solicitud = SOL.solicitudId;
                        solicitudes.Provincia = new etlProvincia
                        {
                            ID_Provincia = SOL.Provincias.provinciaId,
                            Descripcion = SOL.Provincias.provinciaNombre
                        };
                        solicitudes.Cliente = new etlCliente
                        {
                            ID_Cliente = SOL.Clientes.clienteId,
                            Nombre = SOL.Clientes.clienteNombre
                        };
                        solicitudes.Empleado = new etlEmpleado
                        {
                            Cedula = SOL.Empleados.empleadoCedula,
                            Nombre = SOL.Empleados.empleadoNombre
                        };
                        solicitudes.TipoTrabajo.ID_TipoTrabajo = SOL.tipoTrabajoId;
                        solicitudes.Departamento.ID_Departamento = SOL.departamentoId;
                        solicitudes.Equipo.ID_Equipo = SOL.equipoId;
                        solicitudes.Fecha_Reporte = SOL.fechaReporte;
                        solicitudes.horaEntrada = SOL.horaEntrada;
                        solicitudes.horaSalida = SOL.horaSalida;
                        solicitudes.tipoHora = SOL.tipoHora;
                        solicitudes.cantidadHoras = SOL.cantidadHoras;
                        solicitudes.solicitudMotivo = SOL.solicitudMotivo;
                        solicitudes.motivoDetalle = SOL.motivoDetalle;
                        solicitudes.solicitudRepuestos = SOL.solicitudRepuestos;
                        solicitudes.equipoDetenido = SOL.equipoDetenido;
                    }
                }
                return solicitudes;
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnaSolicitudID


        List<Solicitudes> SOLICITUDES = new List<Solicitudes>();
       
         
        public List<Solicitudes> ConsultarUnaSolicitud(string DESCRIPCION)
        {
            

            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    SOLICITUDES = (from x in contextoBD.Solicitudes where x.Empleados.empleadoNombre == DESCRIPCION select x).ToList();
                    return SOLICITUDES;
                   
                }

            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnDepartamento

        public bool ModificarSolicitud(etlSolicitud sol)
        {
            try
            {
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    var SOLICITUD = contextoBD.Solicitudes.SingleOrDefault(b => b.solicitudId == sol.ID_Solicitud);
                    if (SOLICITUD != null)
                    {
                        SOLICITUD.Clientes.clienteId = sol.Cliente.ID_Cliente;
                        SOLICITUD.Provincias.provinciaId = sol.Provincia.ID_Provincia;
                        SOLICITUD.Departamentos.departamentoId = sol.Departamento.ID_Departamento;
                        SOLICITUD.TipoTrabajo.tipoTrabajoId = sol.TipoTrabajo.ID_TipoTrabajo;
                        SOLICITUD.Empleados.empleadoCedula = sol.Empleado.Cedula;
                        SOLICITUD.Equipos.equipoId = sol.Equipo.ID_Equipo;
                        SOLICITUD.fechaReporte = sol.Fecha_Reporte;
                        SOLICITUD.horaEntrada = sol.horaEntrada;
                        SOLICITUD.horaSalida = sol.horaSalida;
                        SOLICITUD.tipoHora = sol.tipoHora;
                        SOLICITUD.cantidadHoras = sol.cantidadHoras;
                        SOLICITUD.solicitudMotivo = sol.solicitudMotivo;
                        SOLICITUD.motivoDetalle = sol.motivoDetalle;
                        SOLICITUD.solicitudRepuestos = sol.solicitudRepuestos;
                        SOLICITUD.equipoDetenido = sol.equipoDetenido;

                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;

            }
            catch (Exception e)
            {
                return false;
            }
        }//FIN DE ModificarEmpleado

    }
}