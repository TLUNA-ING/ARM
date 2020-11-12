using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class SolicitudModelo{

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

        //public void GuardarConsulta(etlSolicitud sol)
        //{
        //    try
        //    {
        //        using (var contextoBD = new ARMEntities())
        //        {
        //            Solicitudes item = new Solicitudes();
        //            item.Clientes.clienteId = sol.Cliente.ID_Cliente;
        //            item.Empleados.empleadoNombre = sol.Empleado.Nombre;
        //            item.Clientes.clienteNombre = sol.Cliente.Descripcion;
        //            item.Equipos.equipoNombre = sol.Equipo.Descripcion;
        //            item.Departamentos.deparatamentoNombre = sol.Departamento.Descripcion;
        //            item.TipoTrabajo.tipoTrabajoNombre = sol.TipoTrabajo.Descripcion;
        //            item.fechaReporte = Convert.ToDateTime(sol.Fecha_Reporte);
        //            item.horaEntrada = Convert.ToDateTime(sol.horaEntrada);
        //            item.horaSalida = Convert.ToDateTime(sol.horaSalida);
        //            item.tipoHora = sol.tipoHora;
        //            item.cantidadHoras = sol.cantidadHoras;
        //            item.solicitudMotivo = sol.solicitudMotivo;
        //            item.motivoDetalle = sol.motivoDetalle;
        //            item.solicitudRepuestos = sol.solicitudRepuestos;
        //            item.equipoDetenido = sol.equipoDetenido;
        //            contextoBD.Solicitudes.Add(item);
        //            contextoBD.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new System.Exception("ID ya existe o hay datos sin llenar");
        //    }

        //}
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