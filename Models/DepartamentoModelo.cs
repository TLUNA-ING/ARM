using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class DepartamentoModelo{
        public List<etlDepartamento> ConsultarTodos(){
            using (var contextoBD = new ARMEntities()){
                var departamentos = contextoBD.Departamentos.Select(x =>
                new etlDepartamento
                {
                    ID_Departamento = x.departamentoId,
                    Descripcion = x.deparatamentoNombre.Trim(),
                    Estado = x.departamentoEstado

                }).ToList();
                return departamentos;
            }
        }//FIN DE ConsultarTodos

        public List<SelectListItem> ConsultarDepartamentosActivos(){
            try {
                using (var contextoBD = new ARMEntities()){

                    var result = (from x in contextoBD.Departamentos where x.departamentoEstado== "Activo" select x).ToList();

                    var itemLista = (from item in result select new SelectListItem { Value = item.departamentoId.ToString(), Text = item.departamentoId.ToString() + " - " + item.deparatamentoNombre }).ToList();

                    List<SelectListItem> ListaDepartamentos = new List<SelectListItem>();

                    ListaDepartamentos.AddRange(itemLista);

                    return ListaDepartamentos.ToList();
                }
            }catch (Exception e){
                throw new System.Exception("");
            }
        }//FIN DE ConsultarClientes

        List<Departamentos> DEPARTAMENTOS = new List<Departamentos>();
        public List<Departamentos> ConsultarUnDepartamento(string DESCRIPCION){
            try{
                using (var contextoBD = new ARMEntities()){
                    DEPARTAMENTOS = (from x in contextoBD.Departamentos where x.deparatamentoNombre == DESCRIPCION select x).ToList();
                    return DEPARTAMENTOS;
                }

            }catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnDepartamento

        public bool AgregarDepartamento(etlDepartamento departamento, long USUARIO){
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities()){
                    Departamentos item = new Departamentos();
                    item.deparatamentoNombre = departamento.Descripcion.Trim();
                    item.departamentoEstado = "Activo";

                    contextoBD.Departamentos.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;

                    string NUEVOS = "Nombre: " + item.deparatamentoNombre + ", Estado: " + item.departamentoEstado;
                    var ACCION = "Inserción de departamento";
                    GuardarEnBitacora(USUARIO, ACCION, null, NUEVOS);
                }
                return AGREGADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE AgregarDepartamento

        public etlDepartamento ConsultarUnDepartamentoID(long ID){
            try{
                etlDepartamento departamento = new etlDepartamento();
                using (var contextoBD = new ARMEntities()){
                    DEPARTAMENTOS = (from x in contextoBD.Departamentos where x.departamentoId == ID select x).ToList();
                    foreach (var DEPART in DEPARTAMENTOS){
                        departamento.ID_Departamento = DEPART.departamentoId;
                        departamento.Descripcion = DEPART.deparatamentoNombre;
                        departamento.Estado = DEPART.departamentoEstado;
                    }
                }
                return departamento;
            }
            catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnDepartamentoID

        public bool ModificarDepartamento(etlDepartamento depart,long USUARIO) {
            try {
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var DEPARTAMENTO = contextoBD.Departamentos.SingleOrDefault(b => b.departamentoId == depart.ID_Departamento);
                    string VIEJOS = "Nombre: " + DEPARTAMENTO.deparatamentoNombre + ", Estado: " + DEPARTAMENTO.departamentoEstado;
                    if (DEPARTAMENTO != null) {
                        DEPARTAMENTO.deparatamentoNombre = depart.Descripcion;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                    string NUEVOS = "Nombre: " + DEPARTAMENTO.deparatamentoNombre + ", Estado: " + DEPARTAMENTO.departamentoEstado;
                    var ACCION = "Modificación de datos de un departamento";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;
            }catch (Exception e) {
                return false;
            }
        }//FIN DE ModificarDepartamento

        public bool ModificarEstado(etlDepartamento depart, long USUARIO){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var DEPARTAMENTO = contextoBD.Departamentos.SingleOrDefault(b => b.departamentoId == depart.ID_Departamento);
                    string VIEJOS = "Nombre: " + DEPARTAMENTO.deparatamentoNombre + ", Estado: " + DEPARTAMENTO.departamentoEstado;
                    
                    if (DEPARTAMENTO != null){

                        if (DEPARTAMENTO.departamentoEstado == "Activo"){
                            DEPARTAMENTO.departamentoEstado = "Inactivo";
                        }else{
                            DEPARTAMENTO.departamentoEstado = "Activo";
                        }
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                    string NUEVOS = "Nombre: " + DEPARTAMENTO.deparatamentoNombre + ", Estado: " + DEPARTAMENTO.departamentoEstado;
                    var ACCION = "Modificación del estado de un departamento";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEstado
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

    }
}