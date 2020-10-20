using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class DepartamentoModelo
    {
        public List<etlDepartamento> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Departamentos.Select(x =>
                new etlDepartamento
                {
                    ID_Departamento = x.departamentoId,
                    Descripcion = x.deparatamentoNombre.Trim()

                }).ToList();
                return respuesta;
            }

        }
        public void GuardarConsulta(etlDepartamento departamento)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Departamentos item = new Departamentos();

                    item.deparatamentoNombre = departamento.Descripcion.Trim();

                    contextoBD.Departamentos.Add(item);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Cedula ya existe");
            }

        }
        public void Eliminar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Departamentos departamentos = contextoBD.Departamentos.Find(id);
                    contextoBD.Departamentos.Remove(departamentos);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlDepartamento Consultar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Departamentos departamentos = contextoBD.Departamentos.Find(id);

                    etlDepartamento departamento = new etlDepartamento
                    {
                        ID_Departamento = departamentos.departamentoId,
                        Descripcion = departamentos.deparatamentoNombre.Trim(),
                    };
                    return departamento;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al consultar");
            }

        }
        public void Actualizar(etlDepartamento departamento)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Departamentos item = contextoBD.Departamentos.Find(departamento.ID_Departamento);

                    item.departamentoId = departamento.ID_Departamento;
                    item.deparatamentoNombre = departamento.Descripcion;
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