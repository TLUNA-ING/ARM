using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ConsultaSolicitud
    {


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


        public void GuardarConsulta(Solicitudes sol)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes item = new Solicitudes();

                    item.solicitudId = sol.solicitudId;
                    item.provinciaId = sol.provinciaId;
                    item.clienteId = sol.clienteId;
                    item.empleadoCedula = sol.empleadoCedula;
                    item.tipoTrabajoId = sol.tipoTrabajoId;
                    item.departamentoId = sol.departamentoId;
                    item.equipoId = sol.equipoId;
                    item.fechaReporte = sol.fechaReporte;
                    item.horaEntrada = sol.horaEntrada;
                    item.tipoHora = sol.tipoHora;
                    item.horaSalida = sol.horaSalida;
                    item.cantidadHoras = sol.cantidadHoras;
                    item.solicitudMotivo = sol.solicitudMotivo;
                    item.motivoDetalle = sol.motivoDetalle;
                    item.solicitudRepuestos = sol.solicitudRepuestos;
                    item.equipoDetenido = sol.equipoDetenido;
                    item.firmaCliente = sol.firmaCliente;
                    contextoBD.Solicitudes.Add(item);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("ID ya existe o hay datos sin llenar");
            }

        }//FIN DE GUARDAR CONSULTA

        public void Actualizar(Solicitudes sol)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes item = contextoBD.Solicitudes.Find(sol.solicitudId);
                    item.clienteId = sol.clienteId;
                    item.empleadoCedula = sol.empleadoCedula;
                    item.tipoTrabajoId = sol.tipoTrabajoId;
                    item.departamentoId = sol.departamentoId;
                    item.equipoId = sol.equipoId;
                    item.fechaReporte = Convert.ToDateTime(sol.fechaReporte);
                    item.horaEntrada = Convert.ToDateTime(sol.horaEntrada);
                    item.tipoHora = sol.tipoHora;
                    item.horaSalida = Convert.ToDateTime(sol.horaSalida);
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
                throw new System.Exception("Error al actualizar");
            }

        }

    }
}