using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ProvinciaModelo
    {
        public List<SelectListItem> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {

                var provincias = (from x in contextoBD.Provincias
                             select x).ToList();

                var itemLista = (from item in provincias
                                 select new SelectListItem { Value = item.provinciaId.ToString(), Text = item.provinciaNombre.ToString() }).ToList();

                List<SelectListItem> Lista_Provincias = new List<SelectListItem>();
                Lista_Provincias.AddRange(itemLista);

                return Lista_Provincias.ToList();
            }
        }
    }
}