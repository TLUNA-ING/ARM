using ProyectoProgramacion.ETL;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class ProvinciaModelo
    {
        public List<etlProvincia> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Provincias.Select(x =>
                new etlProvincia
                {
                    ID_Provincia = x.provinciaId,
                    Descripcion = x.provinciaNombre,

                }).ToList();
                return respuesta;
            }

        }
    }
}