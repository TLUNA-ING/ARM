using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class ClienteModelo
    {
        public List<etlCliente> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Clientes.Select(x =>
                new etlCliente
                {
                    ID_Cliente = x.clienteId,
                    Provincia = new etlProvincia
                    {
                        ID_Provincia = x.Provincias.provinciaId,
                        Descripcion = x.Provincias.provinciaNombre
                    },
                    Descripcion = x.clienteNombre.Trim()

                }).ToList();
                return respuesta;
            }

        }
        public void Guardar(etlCliente cliente)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Clientes item = new Clientes();

                    item.clienteNombre = cliente.Descripcion.Trim();
                    item.provinciaId = cliente.Provincia.ID_Provincia;
                    contextoBD.Clientes.Add(item);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }

        }
        public void Eliminar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Clientes cliente = contextoBD.Clientes.Find(id);
                    contextoBD.Clientes.Remove(cliente);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlCliente Consultar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Clientes item = contextoBD.Clientes.Find(id);

                    etlCliente centro = new etlCliente
                    {
                        ID_Cliente = item.clienteId,
                        Provincia = new etlProvincia
                        {
                            ID_Provincia = item.Provincias.provinciaId
                        },
                        Descripcion = item.clienteNombre.Trim(),
                    };
                    return centro;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al consultar");
            }

        }
        public void Actualizar(etlCliente cliente)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Clientes item = contextoBD.Clientes.Find(cliente.ID_Cliente);

                    item.provinciaId = cliente.Provincia.ID_Provincia;
                    item.clienteNombre = cliente.Descripcion;
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