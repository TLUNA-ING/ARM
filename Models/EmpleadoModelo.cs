using Microsoft.Ajax.Utilities;
using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class EmpleadoModelo {

        public List<etlEmpleado> ConsultarTodos(){
            using (var contextoBD = new ARMEntities()){
                var respuesta = contextoBD.Empleados.Select(x =>
                new etlEmpleado{
                    Cedula = x.empleadoCedula,
                    Nombre = x.empleadoNombre.Trim(),
                    Primer_Apellido = x.empleadoPrimerA.Trim(),
                    Segundo_Apellido = x.empleadoSegundoA.Trim(),
                    Correo = x.empleadoCorreo.Trim(),
                    Estado = x.empleadoEstado.Trim(),
                }).ToList();
                return respuesta;
            }
        }//FIN DE ConsultarTodos

        public bool AgregarEmpleado(etlEmpleado emp){
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities()){
                    Empleados item = new Empleados();
                    item.TipoId = emp.TipoId;
                    item.empleadoCedula = emp.Cedula;
                    item.empleadoNombre = emp.Nombre.Trim();
                    item.empleadoPrimerA = emp.Primer_Apellido.Trim();
                    item.empleadoSegundoA = emp.Segundo_Apellido.Trim();
                    item.empleadoCorreo = emp.Correo.Trim();
                    item.empleadoEstado = "Activo";

                    contextoBD.Empleados.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }

                return AGREGADO;
            }catch (Exception e) {
                return false;
            }
        }//FIN DE AgregarEmpleado

        public etlEmpleado ConsultarUnEmpleadoID(long cedula){
            using (var contextoBD = new ARMEntities()) {

                var result = contextoBD.CONSULTAR_UN_EMPLEADO_BD(cedula.ToString());

                etlEmpleado empleado = new etlEmpleado();

                foreach (var EMP in result){
                    empleado.TipoId = EMP.TipoId;
                    empleado.Cedula = EMP.empleadoCedula;

                    empleado.Nombre = EMP.empleadoNombre;
                    empleado.Primer_Apellido = EMP.empleadoPrimerA;
                    empleado.Segundo_Apellido = EMP.empleadoSegundoA;
                    empleado.Correo = EMP.empleadoCorreo;
                    empleado.Estado = EMP.empleadoEstado;
                }

                return empleado;
            }
        }//FIN DE ConsultarUnEmpleadoID
        
        public bool ModificarEmpleado(etlEmpleado empleado,long USUARIO) {
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var EMPLEADO = contextoBD.Empleados.SingleOrDefault(b => b.empleadoCedula == empleado.Cedula);

                    string VIEJOS = "Nombre: " + EMPLEADO.empleadoCedula + ", Apellido 1: " + EMPLEADO.empleadoPrimerA + ", Apellido 2: " + EMPLEADO.empleadoSegundoA +
                        ", Correo: " + EMPLEADO.empleadoCorreo + ", Estado: " + EMPLEADO.empleadoEstado;
                    

                    if (EMPLEADO != null){
                        EMPLEADO.empleadoNombre = empleado.Nombre;
                        EMPLEADO.empleadoPrimerA = empleado.Primer_Apellido;
                        EMPLEADO.empleadoSegundoA = empleado.Segundo_Apellido;
                        EMPLEADO.empleadoCorreo = empleado.Correo;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;

                    }

                    string NUEVOS = "Nombre: " + EMPLEADO.empleadoCedula + ", Apellido 1: " + EMPLEADO.empleadoPrimerA + ", Apellido 2: " + EMPLEADO.empleadoSegundoA +
                       ", Correo: " + EMPLEADO.empleadoCorreo + ", Estado: " + EMPLEADO.empleadoEstado;
                    var ACCION = "Modificación en tabla Empleado";
                    GuardarEnBitacora(USUARIO,ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;

            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEmpleado

        public bool ModificarEstado(etlEmpleado empleado, long USUARIO){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){

                    var EMPLEADO = contextoBD.Empleados.SingleOrDefault(b => b.empleadoCedula == empleado.Cedula);
                    string VIEJOS = "Nombre: " + EMPLEADO.empleadoCedula + ", Apellido 1: " + EMPLEADO.empleadoPrimerA + ", Apellido 2: " + EMPLEADO.empleadoSegundoA +
                       ", Correo: " + EMPLEADO.empleadoCorreo + ", Estado: " + EMPLEADO.empleadoEstado;

                    if (EMPLEADO != null){

                        if (EMPLEADO.empleadoEstado == "Activo"){
                            EMPLEADO.empleadoEstado = "Inactivo";
                        }else{
                            EMPLEADO.empleadoEstado = "Activo";
                        }
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                    string NUEVOS = "Nombre: " + EMPLEADO.empleadoCedula + ", Apellido 1: " + EMPLEADO.empleadoPrimerA + ", Apellido 2: " + EMPLEADO.empleadoSegundoA +
                      ", Correo: " + EMPLEADO.empleadoCorreo + ", Estado: " + EMPLEADO.empleadoEstado;
                    var ACCION = "Modificación en tabla Empleado";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;

            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEstado

        public List<SelectListItem> ConsultarTipoCedula(){
            try{
                using (var contextoBD = new ARMEntities()){

                    var result = (from x in contextoBD.TipoCedula
                                  select x).ToList();

                    var itemLista = (from item in result
                                     select new SelectListItem { Value = item.TipoId.ToString(), Text = item.TipoDescripcion }).ToList();

                    List<SelectListItem> listaTipoCedula = new List<SelectListItem>();
                    listaTipoCedula.AddRange(itemLista);

                    return listaTipoCedula.ToList();
                }
            }catch (Exception e){
                throw new System.Exception("");
            }
        }//FIN DE ConsultarTipoCedula

        List<Empleados> Empleados = new List<Empleados>();
        public List<Empleados> ConsultarUnEmpleado(long Cedula){
            try{
                using (var contextoBD = new ARMEntities()){
                    Empleados = (from x in contextoBD.Empleados where x.empleadoCedula == Cedula select x).ToList();
                    return Empleados;
                }
            }catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnEmpleado

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