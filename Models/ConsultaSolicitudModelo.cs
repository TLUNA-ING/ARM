using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    
                    Fecha_Reporte = x.fechaReporte.ToString(),
                    horaEntrada = x.horaEntrada.ToString(),
                    horaSalida = x.horaSalida.ToString(),
                    tipoHora = x.tipoHora,
                    cantidadHoras = x.cantidadHoras,
                    solicitudMotivo = x.solicitudMotivo,
                    motivoDetalle = x.motivoDetalle,
                    solicitudRepuestos = x.solicitudRepuestos,
                    equipoDetenido = (int)x.equipoDetenido,
                    tiempoDetenido = x.tiempoDetenido.ToString(),
                    correoMQC = x.correoMQC,
                    nombreMQC = x.nombreMQC,
                    cedulaMQC = x.cedulaMQC

                }).ToList();
                foreach (var SOL in solicitudes){
                    DateTime Fecha = DateTime.Parse(SOL.Fecha_Reporte);
                    SOL.Fecha_Reporte = Fecha.ToString("dd/MM/yyyy");

                    DateTime Fecha_Entrada = DateTime.Parse(SOL.horaEntrada);
                    SOL.horaEntrada = Fecha_Entrada.ToString("hh:mm");

                    DateTime Fecha_Salida = DateTime.Parse(SOL.horaSalida);
                    SOL.horaSalida = Fecha_Salida.ToString("hh:mm");

                    if (SOL.equipoDetenido == 1){
                        SOL.equipoDetenidoS = "Si";
                    }else{
                        SOL.equipoDetenidoS = "No";
                    }
                }
                return solicitudes;
            }
        }//FIN DE ConsultarTodos

        public etlSolicitud ConsultarUnaSolicitudID(long ID){
            try{
                etlSolicitud solicitudes = new etlSolicitud();
                using (var contextoBD = new ARMEntities()){
                   SOLICITUDES  = (from x in contextoBD.Solicitudes where x.solicitudId == ID select x).ToList();
                    foreach (var SOL in SOLICITUDES){
                        solicitudes.ID_Solicitud = SOL.solicitudId;

                        solicitudes.Provincia = new etlProvincia{ID_Provincia = SOL.Provincias.provinciaId,Descripcion = SOL.Provincias.provinciaNombre};
                        solicitudes.Cliente = new etlCliente{ ID_Cliente = SOL.Clientes.clienteId,Nombre = SOL.Clientes.clienteNombre};
                        solicitudes.Empleado = new etlEmpleado{Cedula = SOL.Empleados.empleadoCedula,Nombre = SOL.Empleados.empleadoNombre};

                        solicitudes.TipoTrabajo.ID_TipoTrabajo = SOL.tipoTrabajoId;
                        solicitudes.Departamento.ID_Departamento = SOL.departamentoId;
                        solicitudes.Equipo.ID_Equipo = SOL.equipoId;
                        solicitudes.Fecha_Reporte = SOL.fechaReporte.ToString("yyyy/MM/dd");
                        solicitudes.Fecha_Reporte = solicitudes.Fecha_Reporte.Replace("/", "-");
                        solicitudes.horaEntrada = SOL.horaEntrada.ToString();
                        solicitudes.horaSalida = SOL.horaSalida.ToString();
                        solicitudes.tipoHora = SOL.tipoHora;
                        solicitudes.cantidadHoras = SOL.cantidadHoras;
                        solicitudes.solicitudMotivo = SOL.solicitudMotivo;
                        solicitudes.motivoDetalle = SOL.motivoDetalle;
                        solicitudes.solicitudRepuestos = SOL.solicitudRepuestos;
                        solicitudes.equipoDetenido = (long)SOL.equipoDetenido;
                        solicitudes.tiempoDetenido = SOL.tiempoDetenido.ToString();
                        solicitudes.correoMQC = SOL.correoMQC;
                        solicitudes.nombreMQC = SOL.nombreMQC;
                        solicitudes.cedulaMQC = SOL.cedulaMQC;
                        solicitudes.firmaCliente = null;
                        
                        
                    }
                };
                return solicitudes;
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnaSolicitudID


        List<Solicitudes> SOLICITUDES = new List<Solicitudes>();
      
        public bool ModificarSolicitud(etlSolicitud sol){
            try {
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var SOLICITUD = contextoBD.Solicitudes.SingleOrDefault(b => b.solicitudId == sol.ID_Solicitud);
                    if (SOLICITUD != null){

                        SOLICITUD.clienteId = sol.Cliente.ID_Cliente;
                        SOLICITUD.empleadoCedula = sol.Empleado.Cedula;
                        SOLICITUD.tipoTrabajoId = sol.TipoTrabajo.ID_TipoTrabajo;
                        SOLICITUD.departamentoId = sol.Departamento.ID_Departamento;
                        SOLICITUD.equipoId = sol.Equipo.ID_Equipo;
                        SOLICITUD.fechaReporte = Convert.ToDateTime(sol.Fecha_Reporte);
                        SOLICITUD.horaEntrada = Convert.ToDateTime(sol.horaEntrada);
                        SOLICITUD.horaSalida = Convert.ToDateTime(sol.horaSalida);
                        SOLICITUD.tipoHora = sol.tipoHora;

                        DateTime HoraSalida = Convert.ToDateTime(sol.horaSalida);
                        DateTime HoraEntrada = Convert.ToDateTime(sol.horaEntrada);
                        TimeSpan span = HoraSalida.Subtract(HoraEntrada);
                        string horas = span.Hours.ToString();
                        string minutos = span.Minutes.ToString();
                        if (horas.Length == 1){
                            horas = "0" + horas;
                        }
                        if (minutos.Length == 1){
                            minutos = "0" + minutos;
                        }
                        string CantidadHoras = horas + ":" + minutos;
                        SOLICITUD.cantidadHoras = CantidadHoras;

                        SOLICITUD.solicitudMotivo = sol.solicitudMotivo;
                        SOLICITUD.motivoDetalle = sol.motivoDetalle;
                        SOLICITUD.solicitudRepuestos = sol.solicitudRepuestos;

                        SOLICITUD.equipoDetenido = sol.equipoDetenido;
                        SOLICITUD.tiempoDetenido = sol.tiempoDetenido;
                        SOLICITUD.provinciaId = sol.Provincia.ID_Provincia;
                        SOLICITUD.correoMQC = sol.correoMQC;
                        SOLICITUD.cedulaMQC = sol.cedulaMQC;
                        SOLICITUD.nombreMQC = sol.nombreMQC;
                        SOLICITUD.firmaCliente = SOLICITUD.firmaCliente;

                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEmpleado

    }
}