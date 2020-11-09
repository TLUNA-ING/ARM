using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class EquipoModelo
    {
        public List<etlEquipo> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Equipos.Select(x =>
                new etlEquipo
                {
                    ID_Equipo = x.equipoId,
                    Descripcion = x.equipoNombre.Trim()
                }).ToList();
                return respuesta;
            }

        }
        //public void GuardarConsulta(etlEquipo equipo)
        //{
        //    try
        //    {
        //        using (var contextoBD = new ARMEntities())
        //        {
        //            Equipos item = new Equipos();
        //            item.equipoNombre = equipo.Descripcion.Trim();
        //            item.equipoId = equipo.ID_Equipo;

        //            contextoBD.Equipos.Add(item);
        //            contextoBD.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new System.Exception("Error");
        //    }
        //}

        List<Equipos> EQUIPOS = new List<Equipos>();
        public List<Equipos> ConsultarUnEquipo(string DESCRIPCION) {
            try{
                using (var contextoBD = new ARMEntities())
                {
                    EQUIPOS = (from x in contextoBD.Equipos where x.equipoNombre == DESCRIPCION select x).ToList();
                    return EQUIPOS;
                }

            }
            catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnEmpleado

        public bool GuardarConsulta(etlEquipo equipo)
        {
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    Equipos item = new Equipos();
                    item.equipoNombre = equipo.Descripcion.Trim();

                    contextoBD.Equipos.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }
                return AGREGADO;
            }
            catch (Exception e) {
                return false;
            }
        }



        public void Eliminar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Equipos equipos = contextoBD.Equipos.Find(id);
                    contextoBD.Equipos.Remove(equipos);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlEquipo Consultar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Equipos equipos = contextoBD.Equipos.Find(id);
                    etlEquipo equipo = new etlEquipo
                    {
                        ID_Equipo = equipos.equipoId,
                        Descripcion = equipos.equipoNombre
                    };
                    return equipo;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }

        }
        public void Actualizar(etlEquipo equipo)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Equipos item = contextoBD.Equipos.Find(equipo.ID_Equipo);

                    item.equipoNombre = equipo.Descripcion.Trim();

                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }

        }
    }
}