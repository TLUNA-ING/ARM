using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class SolicitudModelo{


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
        public List<SelectListItem> ConsultarTipoTrabajos(){
            using (var contextoBD = new ARMEntities()){
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
                var result = (from empleado in contextoBD.Empleados select empleado ).ToList();//Consultar todo de la tabla

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









        //    using (var contextoBD = new ARMEntities())
        //    {
        //        var respuesta = contextoBD.Solicitudes.Select(x =>
        //        new etlSolicitud
        //        {
        //            ID_Solicitud = x.solicitudId,
        //            Cliente = new etlCliente
        //            {
        //                ID_Cliente = x.Clientes.clienteId,
        //                Descripcion = x.Clientes.clienteNombre,
        //            },
        //            Departamento = new etlDepartamento
        //            {
        //                ID_Departamento = x.Departamentos.departamentoId,
        //                Descripcion = x.Departamentos.deparatamentoNombre,
        //            },
        //            Equipo = new etlEquipo
        //            {
        //                ID_Equipo = x.Equipos.equipoId,
        //                Descripcion = x.Equipos.equipoNombre,
        //            },
        //            Empleado = new etlEmpleado
        //            {
        //                Cedula = x.Empleados.empleadoCedula,
        //                Nombre = x.Empleados.empleadoNombre,
        //            },
        //            TipoTrabajo = new etlTipoTrabajo
        //            {
        //                ID_TipoTrabajo = x.TipoTrabajo.tipoTrabajoId,
        //                Descripcion = x.TipoTrabajo.tipoTrabajoNombre,
        //            },

        //            Fecha_Reporte = x.fechaReporte,
        //            //Reporte = x.solicitudMotivo.Trim(),
        //            //Fecha = x.fechaReporte.ToString(),
        //            horaEntrada = x.horaEntrada,
        //            horaSalida = x.horaSalida,
        //            tipoHora = x.tipoHora,
        //            cantidadHoras = x.cantidadHoras,
        //            solicitudMotivo = x.solicitudMotivo,
        //            motivoDetalle = x.motivoDetalle,
        //            solicitudRepuestos = x.solicitudRepuestos,
        //            equipoDetenido = x.equipoDetenido


        //        }).ToList();
        //        return respuesta;
        //    }

        //}

        public void GuardarConsulta(etlSolicitud sol)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes item = new Solicitudes();
                    //item.Clientes.clienteId = sol.Cliente.ID_Cliente;
                    item.Empleados.empleadoNombre = sol.Empleado.Nombre;
                    item.Clientes.clienteNombre = sol.Cliente.Descripcion;
                    item.Equipos.equipoNombre = sol.Equipo.Descripcion;
                    item.Departamentos.deparatamentoNombre = sol.Departamento.Descripcion;
                    item.TipoTrabajo.tipoTrabajoNombre = sol.TipoTrabajo.Descripcion;
                    item.fechaReporte = Convert.ToDateTime(sol.Fecha_Reporte);
                    item.horaEntrada = Convert.ToDateTime(sol.horaEntrada);
                    item.horaSalida = Convert.ToDateTime(sol.horaSalida);
                    item.tipoHora = sol.tipoHora;
                    item.cantidadHoras = sol.cantidadHoras;
                    item.solicitudMotivo = sol.solicitudMotivo;
                    item.motivoDetalle = sol.motivoDetalle;
                    item.solicitudRepuestos = sol.solicitudRepuestos;
                    item.equipoDetenido = sol.equipoDetenido;
                    contextoBD.Solicitudes.Add(item);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("ID ya existe o hay datos sin llenar");
            }

        }
        //public void Eliminar(string id)
        //{
        //    try
        //    {
        //        using (var contextoBD = new ARMEntities())
        //        {
        //            Solicitudes sol = contextoBD.Solicitudes.Find(id);
        //            contextoBD.Solicitudes.Remove(sol);
        //            contextoBD.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new System.Exception("Error al eliminar");
        //    }

        //}

        //public etlSolicitud Consultar(string id)
        //{
        //    try
        //    {
        //        using (var contextoBD = new ARMEntities())
        //        {
        //            Solicitudes x = contextoBD.Solicitudes.Find(id);
        //            etlSolicitud solicitud = new etlSolicitud
        //            {
        //                ID_Solicitud = x.solicitudId,
        //                Cliente = new etlCliente
        //                {
        //                    ID_Cliente = x.Clientes.clienteId,
        //                    Descripcion = x.Clientes.clienteNombre,
        //                },
        //                Departamento = new etlDepartamento
        //                {
        //                    ID_Departamento = x.Departamentos.departamentoId,
        //                    Descripcion = x.Departamentos.deparatamentoNombre,
        //                },
        //                Equipo = new etlEquipo
        //                {
        //                    ID_Equipo = x.Equipos.equipoId,
        //                    Descripcion = x.Equipos.equipoNombre,
        //                },
        //                Empleado = new etlEmpleado
        //                {
        //                    Cedula = x.Empleados.empleadoCedula,
        //                    Nombre = x.Empleados.empleadoNombre,
        //                },
        //                TipoTrabajo = new etlTipoTrabajo
        //                {
        //                    ID_TipoTrabajo = x.TipoTrabajo.tipoTrabajoId,
        //                    Descripcion = x.TipoTrabajo.tipoTrabajoNombre,
        //                },
        //                Fecha_Reporte = x.fechaReporte,
        //                //Reporte = x.solicitudMotivo.Trim(),
        //                //Fecha = x.fechaReporte.ToString(),
        //                horaEntrada = x.horaEntrada,
        //                horaSalida = x.horaSalida,
        //                tipoHora = x.tipoHora,
        //                cantidadHoras = x.cantidadHoras,
        //                solicitudMotivo = x.solicitudMotivo,
        //                motivoDetalle = x.motivoDetalle,
        //                solicitudRepuestos = x.solicitudRepuestos,
        //                equipoDetenido = x.equipoDetenido,


        //            };
        //            solicitud.Fecha_Reporte.ToString("yyyy-MM-dd");
        //            return solicitud;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new System.Exception("Error al eliminar");
        //    }

        //}
        //public void Actualizar(etlSolicitud sol)
        //{
        //    try
        //    {
        //        using (var contextoBD = new ARMEntities())
        //        {
        //            Solicitudes item = contextoBD.Solicitudes.Find(sol.ID_Solicitud);

        //            item.Empleados.empleadoCedula = sol.Empleado.Cedula;
        //            item.Clientes.clienteNombre = sol.Cliente.Descripcion;
        //            item.Equipos.equipoNombre = sol.Equipo.Descripcion;
        //            item.Departamentos.deparatamentoNombre = sol.Departamento.Descripcion;
        //            item.TipoTrabajo.tipoTrabajoNombre = sol.TipoTrabajo.Descripcion;
        //            item.fechaReporte = Convert.ToDateTime(sol.Fecha_Reporte);
        //            //item.solicitudMotivo = sol.Reporte;
        //            //item.fechaReporte = Convert.ToDateTime(sol.Fecha);
        //            item.horaEntrada = Convert.ToDateTime(sol.horaEntrada);
        //            item.horaSalida = Convert.ToDateTime(sol.horaSalida);
        //            item.tipoHora = sol.tipoHora;
        //            item.cantidadHoras = sol.cantidadHoras;
        //            item.solicitudMotivo = sol.solicitudMotivo;
        //            item.motivoDetalle = sol.motivoDetalle;
        //            item.solicitudRepuestos = sol.solicitudRepuestos;
        //            item.equipoDetenido = sol.equipoDetenido;
        //            contextoBD.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new System.Exception("Error al actualizar");
        //    }

        //}
    }
}