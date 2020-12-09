using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ConsultaSolicitud{

        public List<SelectListItem> ConsultarClientes(int ID_PROVINCIA){
            using (var contextoBD = new ARMEntities()){
                var result = (from cliente in contextoBD.Clientes where cliente.provinciaId == ID_PROVINCIA && cliente.clienteEstado=="Activo" select cliente).ToList();

                var itemLista = (from item in result select new SelectListItem { Value = item.clienteId.ToString(), Text = item.clienteNombre }).ToList();
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

        public List<SelectListItem> ConsultarTipoTrabajos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var result = (from tipo in contextoBD.TipoTrabajo where tipo.tipoTrabajoEstado == "Activo" select tipo).ToList();//Consultar todo de la tabla

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
                var result = (from empleado in contextoBD.Empleados where empleado.empleadoEstado == "Activo" select empleado).ToList();//Consultar todo de la tabla

                var itemLista = (from item in result
                                 select new SelectListItem { Value = item.empleadoCedula.ToString(), Text = item.empleadoNombre+' '+item.empleadoPrimerA }).ToList();

                List<SelectListItem> listaEmpleado = new List<SelectListItem>();
                listaEmpleado.AddRange(itemLista);

                return listaEmpleado.ToList();
            }
        }//FIN DE ConsultarEmpleados

        public List<SelectListItem> ConsultarDepartamentos(int ID_CLIENTE){
            using (var contextoBD = new ARMEntities()){
                var result = (from departamento in contextoBD.Departamento_X_Cliente where departamento.Clientes.clienteId == ID_CLIENTE && departamento.Departamentos.departamentoEstado=="Activo" select departamento).ToList();
                var itemLista = (from item in result select new SelectListItem { Value = item.departamentoId.ToString(), Text = item.Departamentos.deparatamentoNombre }).ToList();

                List<SelectListItem> listaDepartamento = new List<SelectListItem>();
                listaDepartamento.AddRange(itemLista);

                return listaDepartamento.ToList();
            }
        }//FIN DE ConsultarDepartamentos

        public List<SelectListItem> ConsultarEquipos(int ID_DEPARTAMENTO){
            using (var contextoBD = new ARMEntities()){
                var result = (from equipo in contextoBD.Equipo_X_Departamento where equipo.departamentoId == ID_DEPARTAMENTO && equipo.Departamentos.departamentoEstado=="Activo" select equipo).ToList();
                var itemLista = (from item in result select new SelectListItem { Value = item.equipoId.ToString(), Text = item.Equipos.equipoNombre }).ToList();

                List<SelectListItem> listaEquipo = new List<SelectListItem>();
                listaEquipo.AddRange(itemLista);

                return listaEquipo.ToList();
            }
        }//FIN DE ConsultarEquipos

        public void GuardarConsulta(Solicitudes sol, long USUARIO){
            try{
                using (var contextoBD = new ARMEntities()){

                    Solicitudes item = new Solicitudes();

                    item.solicitudId = sol.solicitudId;
                    item.clienteId = sol.clienteId;
                    item.empleadoCedula = sol.empleadoCedula;
                    item.tipoTrabajoId = sol.tipoTrabajoId;
                    item.departamentoId = sol.departamentoId;
                    item.equipoId = sol.equipoId;
                    item.fechaReporte = sol.fechaReporte;
                    item.horaEntrada = sol.horaEntrada;         
                    item.horaSalida = sol.horaSalida;
                    item.tipoHora = sol.tipoHora;

                    DateTime HoraSalida = DateTime.Parse(sol.horaSalida);
                    DateTime HoraEntrada = DateTime.Parse(sol.horaEntrada);
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


                    item.cantidadHoras = CantidadHoras;
                    item.solicitudMotivo = sol.solicitudMotivo;
                    item.motivoDetalle = sol.motivoDetalle;

                    item.solicitudRepuestos = sol.solicitudRepuestos;
                    item.equipoDetenido = sol.equipoDetenido;
                    item.tiempoDetenido = sol.tiempoDetenido;

                    item.provinciaId = sol.provinciaId;
                    item.firmaCliente = sol.firmaCliente;
                    item.cedulaMQC = sol.cedulaMQC;
                    item.correoMQC = sol.correoMQC;
                    item.nombreMQC = sol.nombreMQC;
                    contextoBD.Solicitudes.Add(item);
                    contextoBD.SaveChanges();

                    string NUEVOS = "Cliente: " + item.clienteId + ", Empleado: " + item.empleadoCedula + ", Tipo Trabajo: " + item.tipoTrabajoId +
                       ", Departamento: " + item.departamentoId + ", Equipo: " + item.equipoId + ", Fecha: " + item.fechaReporte +
                        ", Hora Entrada: " + item.horaEntrada + ", Tipo Hora: " + item.tipoHora + ", Hora Salida: " + item.horaSalida +
                         ", Horas: " + item.cantidadHoras + ", Motivo: " + item.solicitudMotivo + ", Detalle: " + item.motivoDetalle +
                          ", Repuestos: " + item.solicitudRepuestos + ", E_Detenido: " + item.equipoDetenido;

                    var ACCION = "Inserción de solicitud";
                    GuardarEnBitacora(USUARIO, ACCION, null, NUEVOS);
                }
            }catch (Exception e){
                throw new System.Exception("ID ya existe o hay datos sin llenar");
            }
        }//FIN DE GUARDAR CONSULTA

        public bool GuardarEnBitacora(long USUARIO, string ACCION, string VIEJOS, string NUEVOS)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    var result = contextoBD.INSERTAR_EN_BITACORA(USUARIO, ACCION, VIEJOS, NUEVOS);

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }//FIN DE INGRESO EN BITACORA

    }
}