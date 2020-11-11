using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoProgramacion.Models
{
    public class TipoTrabajoModelo{

        public List<etlTipoTrabajo> ConsultarTodos(){
            using (var contextoBD = new ARMEntities()){
                var tipos = contextoBD.TipoTrabajo.Select(x =>
                new etlTipoTrabajo {
                    ID_TipoTrabajo = x.tipoTrabajoId,
                    Descripcion = x.tipoTrabajoNombre.Trim(),
                    Estado = x.tipoTrabajoEstado
                }).ToList();
                return tipos;
            }
        }//FIN DE ConsultarTodos
        List<TipoTrabajo> TIPOS = new List<TipoTrabajo>();
        public List<TipoTrabajo> ConsultarUnTipoTrabajo(string DESCRIPCION){
            try{
                using (var contextoBD = new ARMEntities()){
                    TIPOS = (from x in contextoBD.TipoTrabajo where x.tipoTrabajoNombre == DESCRIPCION select x).ToList();
                    return TIPOS;
                }

            }catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnTipoTrabajo

        public bool AgregarTipoTrabajo(etlTipoTrabajo tipo){
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities()){
                    TipoTrabajo item = new TipoTrabajo();
                    item.tipoTrabajoNombre = tipo.Descripcion.Trim();
                    item.tipoTrabajoEstado = "Activo";

                    contextoBD.TipoTrabajo.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }
                return AGREGADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE AgregarTipoTrabajo

        public etlTipoTrabajo ConsultarUnTipoTrabajoID(long ID){
            try{
                etlTipoTrabajo tipo = new etlTipoTrabajo();
                using (var contextoBD = new ARMEntities()){
                    TIPOS = (from x in contextoBD.TipoTrabajo where x.tipoTrabajoId == ID select x).ToList();
                    foreach (var EQUIP in TIPOS){
                        tipo.ID_TipoTrabajo = EQUIP.tipoTrabajoId;
                        tipo.Descripcion = EQUIP.tipoTrabajoNombre;
                        tipo.Estado = EQUIP.tipoTrabajoEstado;
                    }
                }
                return tipo;
            }
            catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnEquipoID

        public bool ModificarTipoTrabajo(etlTipoTrabajo tip) {
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()) {
                    var TIPO = contextoBD.TipoTrabajo.SingleOrDefault(b => b.tipoTrabajoId == tip.ID_TipoTrabajo);
                    if (TIPO != null){
                        TIPO.tipoTrabajoNombre = tip.Descripcion;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                }
                return MODIFICADO;
            } catch (Exception e){
                return false;
            }
        }//FIN DE ModificarTipoTrabajo

        public bool ModificarEstado(etlTipoTrabajo tip){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()) {
                    var TIPO = contextoBD.TipoTrabajo.SingleOrDefault(b => b.tipoTrabajoId == tip.ID_TipoTrabajo);
                    if (TIPO != null){

                        if (TIPO.tipoTrabajoEstado == "Activo"){
                            TIPO.tipoTrabajoEstado = "Inactivo";
                        } else{
                            TIPO.tipoTrabajoEstado = "Activo";
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

    }//FIN DE TipoTrabajoModelo
}