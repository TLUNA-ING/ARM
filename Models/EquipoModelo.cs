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
                    Descripcion = x.equipoNombre.Trim(),
                    Estado = x.equipoEstado
                }).ToList();
                return respuesta;
            }

        }

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
        }//FIN DE ConsultarUnEquipo

        public etlEquipo ConsultarUnEquipoID(long ID){
            try {
                etlEquipo equipo = new etlEquipo();
                using (var contextoBD = new ARMEntities()){
                    EQUIPOS = (from x in contextoBD.Equipos where x.equipoId == ID select x).ToList();
                    foreach (var EQUIP in EQUIPOS){
                        equipo.ID_Equipo = EQUIP.equipoId;
                        equipo.Descripcion = EQUIP.equipoNombre;
                        equipo.Estado = EQUIP.equipoEstado;
                    }
                }
                return equipo;
            }
            catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnEquipoID


        public bool ModificarEstado(etlEquipo equipo){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var EQUIPO = contextoBD.Equipos.SingleOrDefault(b => b.equipoId == equipo.ID_Equipo);
                    if (EQUIPO != null){

                        if (EQUIPO.equipoEstado =="Activo"){
                            EQUIPO.equipoEstado = "Inactivo";
                        }else{
                            EQUIPO.equipoEstado = "Activo";
                        }
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;

            } catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEstado

        public bool ModificarEquipo(etlEquipo equip){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    var EQUIPO = contextoBD.Equipos.SingleOrDefault(b => b.equipoId == equip.ID_Equipo);
                    if (EQUIPO != null){
                        EQUIPO.equipoNombre = equip.Descripcion;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;

            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEquipo


        public bool AgregarEquipo(etlEquipo equipo){
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    Equipos item = new Equipos();
                    item.equipoNombre = equipo.Descripcion.Trim();
                    item.equipoEstado = "Activo";

                    contextoBD.Equipos.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }
                return AGREGADO;
            }
            catch (Exception e) {
                return false;
            }
        }//FIN DE AgregarEquipo



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
    }
}