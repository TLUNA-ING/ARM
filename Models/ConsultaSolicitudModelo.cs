using Microsoft.OData.Edm;
using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Windows.Documents;

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
                    ID_Solicitud = (int)x.solicitudId,
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

        public Solicitudes ConsultarUnaSolicitudID(long ID)
        {
            try
            {
                Solicitudes solicitudes = new Solicitudes();
                using (var contextoBD = new ARMEntities())
                {
                   SOLICITUDES  = (from x in contextoBD.Solicitudes where x.solicitudId == ID select x).ToList();
                    foreach (var SOL in SOLICITUDES)
                    {
                        solicitudes.solicitudId = SOL.solicitudId;
                        solicitudes.clienteId = SOL.clienteId;
                        solicitudes.tipoTrabajoId = SOL.tipoTrabajoId;
                        solicitudes.departamentoId = SOL.departamentoId;
                        solicitudes.equipoId = SOL.equipoId;
                        solicitudes.fechaReporte = SOL.fechaReporte;
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
        }//FIN DE ConsultarUnDepartamentoID


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

     
    }
}