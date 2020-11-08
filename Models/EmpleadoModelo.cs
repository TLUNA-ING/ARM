using Microsoft.Ajax.Utilities;
using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class EmpleadoModelo {

        public List<SelectListItem> CONSULTAR_TIPO_CED(){
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

            }catch (Exception e) {
                throw new System.Exception("");
            }
        }


        public List<etlEmpleado> ConsultarTodos()
        {
            using (var contextoBD = new ARMEntities())
            {
                var respuesta = contextoBD.Empleados.Select(x =>
                new etlEmpleado
                {
                    Cedula = x.empleadoCedula,
                    Nombre = x.empleadoNombre.Trim(),
                    Primer_Apellido = x.empleadoPrimerA.Trim(),
                    Segundo_Apellido = x.empleadoSegundoA.Trim(),
                    Correo = x.empleadoCorreo.Trim(),

                }).ToList();
                return respuesta;
            }

        }
        public bool GuardarConsulta(etlEmpleado emp){
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

                    contextoBD.Empleados.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }
                return AGREGADO;
            }catch (Exception e) {
                return false;
            }
        }
        public void Eliminar(long id)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Empleados empleado = contextoBD.Empleados.Find(id);
                    contextoBD.Empleados.Remove(empleado);
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }

        public etlEmpleado Consultar(long cedula)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Empleados emp = contextoBD.Empleados.Find(cedula);
                    etlEmpleado empleado = new etlEmpleado
                    {
                        Cedula = emp.empleadoCedula,
                        Nombre = emp.empleadoNombre.Trim(),
                        Primer_Apellido = emp.empleadoPrimerA.Trim(),
                        Segundo_Apellido = emp.empleadoSegundoA.Trim(),
                        Correo = emp.empleadoCorreo.Trim()

                    };
                    return empleado;
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al eliminar");
            }

        }
        public void Actualizar(etlEmpleado emp)
        {
            try
            {
                using (var contextoBD = new ARMEntities())
                {
                    Empleados item = contextoBD.Empleados.Find(emp.Cedula);

                    item.empleadoNombre = emp.Nombre;
                    item.empleadoPrimerA = emp.Primer_Apellido;
                    item.empleadoSegundoA = emp.Segundo_Apellido;
                    item.empleadoCorreo = emp.Correo;
                    contextoBD.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new System.Exception("Error al actualizar");
            }

        }

        List<Empleados> EMPLEADOS = new List<Empleados>();
        public List<Empleados> ConsultarUnEmpleado(long cedula){
            try{

                using (var contextoBD = new ARMEntities()){
                    EMPLEADOS = (from x in contextoBD.Empleados where x.empleadoCedula == cedula select x).ToList();                
                    return EMPLEADOS;
                }

            }catch (Exception e){
                throw new System.Exception("Error al eliminar");
            }
        }//FIN DE ConsultarUnEmpleado


        public etlEmpleado CONSULTAR_UN_EMPLEADO_BD(long cedula){
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
                }

                return empleado;
            }
        }


    }
}