using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class TipoTrabajoModelo
    {
        public List<etlTipoTrabajo> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.TipoTrabajo.Select(x =>
                new etlTipoTrabajo
                {
                    ID_TipoTrabajo = x.tipoTrabajoId,
                    Descripcion = x.tipoTrabajoNombre.Trim()

                }).ToList();
                return respuesta;
            }

        }
        public void Guardar(etlTipoTrabajo tipoTrabajo)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    TipoTrabajo item = new TipoTrabajo();

                    item.tipoTrabajoNombre = tipoTrabajo.Descripcion.Trim();
                    item.tipoTrabajoId = tipoTrabajo.ID_TipoTrabajo;
                    contextoBD.TipoTrabajo.Add(item);
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
                    TipoTrabajo tipoTrabajo = contextoBD.TipoTrabajo.Find(id);
                    contextoBD.TipoTrabajo.Remove(tipoTrabajo);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlTipoTrabajo Consultar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    TipoTrabajo item = contextoBD.TipoTrabajo.Find(id);

                    etlTipoTrabajo tipoTrabajo = new etlTipoTrabajo
                    {
                        ID_TipoTrabajo = item.tipoTrabajoId,
                        Descripcion = item.tipoTrabajoNombre.Trim(),
                    };
                    return tipoTrabajo;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al consultar");
            }

        }
        public void Actualizar(etlTipoTrabajo tipoTrabajo)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    TipoTrabajo item = contextoBD.TipoTrabajo.Find(tipoTrabajo.ID_TipoTrabajo);

                    item.tipoTrabajoId = tipoTrabajo.ID_TipoTrabajo;
                    item.tipoTrabajoNombre = tipoTrabajo.Descripcion;
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