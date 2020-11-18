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

        public List<SelectListItem> ConsultarProvincias()
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {

                    var result = (from x in contextoBD.Provincias select x).ToList();

                    var itemLista = (from item in result select new SelectListItem { Value = item.provinciaId.ToString(), Text = item.provinciaNombre }).ToList();

                    List<SelectListItem> ListaProvincias = new List<SelectListItem>();

                    ListaProvincias.AddRange(itemLista);

                    return ListaProvincias.ToList();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("");
            }
        }//FIN DE ConsultarProvincias
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
                        solicitudes.Provincia.Descripcion = SOL.Provincias.provinciaNombre;
                        solicitudes.Cliente.Nombre = SOL.Clientes.clienteNombre;
                        solicitudes.Empleado.Nombre = SOL.Empleados.empleadoNombre;
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

        public bool ModificarSolicitud(etlSolicitud solicitud)
        {
            try
            {
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    var SOLICITUD = contextoBD.Solicitudes.SingleOrDefault(b => b.solicitudId == solicitud.ID_Solicitud);
                    if (SOLICITUD != null)
                    {
                        SOLICITUD.Clientes.clienteNombre = solicitud.Cliente.Nombre;
                        SOLICITUD.Provincias.provinciaNombre = solicitud.Provincia.Descripcion;
                        SOLICITUD.Departamentos.deparatamentoNombre = solicitud.Departamento.Descripcion;
                        SOLICITUD.TipoTrabajo.tipoTrabajoNombre = solicitud.TipoTrabajo.Descripcion;
                        SOLICITUD.Empleados.empleadoNombre = solicitud.Empleado.Nombre;
                        SOLICITUD.Equipos.equipoNombre = solicitud.Equipo.Descripcion;
                        SOLICITUD.fechaReporte = solicitud.Fecha_Reporte;
                        SOLICITUD.horaEntrada = solicitud.horaEntrada;
                        SOLICITUD.horaSalida = solicitud.horaSalida;
                        SOLICITUD.tipoHora = solicitud.tipoHora;
                        SOLICITUD.cantidadHoras = solicitud.cantidadHoras;
                        SOLICITUD.solicitudMotivo = solicitud.solicitudMotivo;
                        SOLICITUD.motivoDetalle = solicitud.motivoDetalle;
                        SOLICITUD.solicitudRepuestos = solicitud.solicitudRepuestos;
                        SOLICITUD.equipoDetenido = solicitud.equipoDetenido;

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