using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class EquipoModelo{
        public List<etlEquipo> ConsultarTodos(){
            using (var contextoBD = new ARMEntities()){
                var respuesta = contextoBD.Equipos.Select(x =>
                new etlEquipo{
                    ID_Equipo = x.equipoId,
                    Descripcion = x.equipoNombre.Trim(),
                    Estado = x.equipoEstado
                }).ToList();
                return respuesta;
            }
        }//FIN DE ConsultarTodos

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

        public bool ModificarEstado(etlEquipo equipo,long USUARIO){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var EQUIPO = contextoBD.Equipos.SingleOrDefault(b => b.equipoId == equipo.ID_Equipo);
                    string VIEJOS = "Nombre: " + EQUIPO.equipoNombre + ", Estado: " + EQUIPO.equipoEstado;

                    if (EQUIPO != null){

                        if (EQUIPO.equipoEstado =="Activo"){
                            EQUIPO.equipoEstado = "Inactivo";
                        }else{
                            EQUIPO.equipoEstado = "Activo";
                        }
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                    string NUEVOS = "Nombre: " + EQUIPO.equipoNombre + ", Estado: " + EQUIPO.equipoEstado;
                    var ACCION = "Modificación en tabla Equipos";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;

            } catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEstado

        public bool ModificarEquipo(etlEquipo equip, long USUARIO){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    var EQUIPO = contextoBD.Equipos.SingleOrDefault(b => b.equipoId == equip.ID_Equipo);
                    string VIEJOS = "Nombre: " + EQUIPO.equipoNombre + ", Estado: " + EQUIPO.equipoEstado;

                    if (EQUIPO != null){
                        EQUIPO.equipoNombre = equip.Descripcion;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                    string NUEVOS = "Nombre: " + EQUIPO.equipoNombre + ", Estado: " + EQUIPO.equipoEstado;
                    var ACCION = "Modificación en tabla Equipos";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
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
            } catch (Exception e) {
                return false;
            }
        }//FIN DE AgregarEquipo
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

    }//FIN DE EquipoModelo

}