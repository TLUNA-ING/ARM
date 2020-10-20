using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class SolicitudModelo
    {
        public List<etlSolicitud> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Solicitudes.Select(x =>
                new etlSolicitud
                {
                    ID_Solicitud = x.solicitudId,
                    Cliente = new etlCliente
                    {
                        ID_Cliente = x.Clientes.clienteId,
                        Descripcion = x.Clientes.clienteNombre.Trim()
                    },
                    Departamento = new etlDepartamento
                    {
                        ID_Departamento = x.Departamentos.departamentoId,
                        Descripcion = x.Departamentos.deparatamentoNombre.Trim()
                    },
                    Cedula = x.Empleados.empleadoCedula,
                    Fecha_Reporte = x.fechaReporte,
                    Reporte = x.solicitudMotivo.Trim(),
                    Fecha = x.fechaReporte.ToString()

                }).ToList();
                return respuesta;
            }

        }

        public void GuardarConsulta(etlSolicitud sol)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes item = new Solicitudes();
                    item.Empleados.empleadoCedula = sol.Cedula;
                    item.fechaReporte = Convert.ToDateTime(sol.Fecha_Reporte);
                    item.solicitudMotivo = sol.Reporte;
                    item.clienteId = sol.Cliente.ID_Cliente;
                    item.departamentoId = sol.Departamento.ID_Departamento;
                    contextoBD.Solicitudes.Add(item);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("ID ya existe o hay datos sin llenar");
            }

        }
        public void Eliminar(string id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes sol = contextoBD.Solicitudes.Find(id.Trim());
                    contextoBD.Solicitudes.Remove(sol);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlSolicitud Consultar(string id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes x = contextoBD.Solicitudes.Find(id.Trim());
                    etlSolicitud solicitud = new etlSolicitud
                    {
                        ID_Solicitud = x.solicitudId,
                        Cliente = new etlCliente
                        {
                            ID_Cliente = x.Clientes.clienteId,
                            Descripcion = x.Clientes.clienteNombre.Trim(),
                            Provincia = new etlProvincia
                            {
                                ID_Provincia = x.Clientes.Provincias.provinciaId
                            }
                        },
                        Departamento = new etlDepartamento
                        {
                            ID_Departamento = x.Departamentos.departamentoId,
                            Descripcion = x.Departamentos.deparatamentoNombre.Trim()
                        },
                        Cedula = x.Empleados.empleadoCedula,
                        Fecha_Reporte = x.fechaReporte,
                        Reporte = x.solicitudMotivo.Trim()


                    };
                    solicitud.Fecha = solicitud.Fecha_Reporte.ToString("yyyy-MM-dd");
                    return solicitud;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }
        public void Actualizar(etlSolicitud sol)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Solicitudes item = contextoBD.Solicitudes.Find(sol.ID_Solicitud);

                    item.Empleados.empleadoCedula = sol.Cedula;
                    item.Clientes.clienteNombre = sol.Clientes.Trim();
                    item.fechaReporte = Convert.ToDateTime(sol.Fecha_Reporte);
                    item.solicitudMotivo = sol.Reporte;
                    item.clienteId = sol.Cliente.ID_Cliente;
                    item.departamentoId = sol.Departamento.ID_Departamento;
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