﻿using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoProgramacion.Models
{
    public class ClienteModelo{
        public List<etlCliente> ConsultarTodos(){
            using (var contextoBD = new ARMEntities()){
                var respuesta = contextoBD.Clientes.Select(x =>
                new etlCliente{
                    ID_Cliente = x.clienteId,
                    Provincia = new etlProvincia{
                        ID_Provincia = x.Provincias.provinciaId,
                        Descripcion = x.Provincias.provinciaNombre
                    },
                    Nombre = x.clienteNombre.Trim(),
                    Correo = x.clienteCorreo.Trim(),
                    Estado = x.clienteEstado
                }).ToList();
                return respuesta;
            }
        }//FIN DE ConsultarTodos

        public List<SelectListItem> ConsultarProvincias(){
            try{
                using (var contextoBD = new ARMEntities()){

                    var result = (from x in contextoBD.Provincias select x).ToList();

                    var itemLista = (from item in result select new SelectListItem { Value = item.provinciaId.ToString(), Text = item.provinciaNombre }).ToList();

                    List<SelectListItem> ListaProvincias = new List<SelectListItem>();

                    ListaProvincias.AddRange(itemLista);

                    return ListaProvincias.ToList();
                }
            }catch (Exception e){
                throw new System.Exception("");
            }
        }//FIN DE ConsultarProvincias


        List<Clientes> CLIENTES = new List<Clientes>();
        public List<Clientes> ConsultarUnCliente(string NOMBRE){
            try{
                using (var contextoBD = new ARMEntities()){
                    CLIENTES = (from x in contextoBD.Clientes where x.clienteNombre == NOMBRE select x).ToList();
                    return CLIENTES;
                }

            }catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnCliente

        public bool AgregarCliente(etlCliente cliente,long USUARIO){
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities()){
                    Clientes item = new Clientes();

                    item.provinciaId = cliente.Provincia.ID_Provincia;
                    item.clienteNombre = cliente.Nombre;
                    item.clienteCorreo = cliente.Correo;
                    item.clienteEstado = "Activo";

                    contextoBD.Clientes.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;

                    string NUEVOS = "Provincia: " + item.provinciaId + ", Nombre: " + item.clienteNombre + ", Correo: " + item.clienteCorreo +
                               ", Estado: " + item.clienteEstado;
                    var ACCION = "Inserción de cliente";
                    GuardarEnBitacora(USUARIO, ACCION, null, NUEVOS);
                }                

                return AGREGADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE AgregarCliente

        public etlCliente ConsultarUnClienteID(long ID){
            try{
                etlCliente cliente = new etlCliente();
                using (var contextoBD = new ARMEntities()){
                    CLIENTES = (from x in contextoBD.Clientes where x.clienteId == ID select x).ToList();
                    foreach (var CLI in CLIENTES){

                        cliente.ID_Cliente = CLI.clienteId;
                        cliente.Nombre = CLI.clienteNombre;
                        cliente.Correo = CLI.clienteCorreo;
                        cliente.Estado = CLI.clienteEstado;
                        cliente.Provincia = new etlProvincia
                        {
                            ID_Provincia = CLI.Provincias.provinciaId,
                            Descripcion = CLI.Provincias.provinciaNombre
                        };
                    }
                }
                return cliente;
            }
            catch (Exception e){
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarUnClienteID

        public bool ModificarCliente(etlCliente cli,long USUARIO){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var CLIENTE = contextoBD.Clientes.SingleOrDefault(b => b.clienteId == cli.ID_Cliente);
                    string VIEJOS = "Provincia: " + CLIENTE.provinciaId + ", Nombre: " + CLIENTE.clienteNombre + ", Correo: " + CLIENTE.clienteCorreo +
                        ", Estado: " + CLIENTE.clienteEstado;
                    
                    if (CLIENTE != null){
                        CLIENTE.provinciaId = cli.Provincia.ID_Provincia;
                        CLIENTE.clienteNombre = cli.Nombre;
                        CLIENTE.clienteCorreo = cli.Correo;
                        contextoBD.SaveChanges();
                        MODIFICADO = true;
                    }
                    string NUEVOS = "Provincia: " + CLIENTE.provinciaId + ", Nombre: " + CLIENTE.clienteNombre + ", Correo: " + CLIENTE.clienteCorreo +
                        ", Estado: " + CLIENTE.clienteEstado;
                    var ACCION = "Modificación de datos en un cliente";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarCliente

        public bool ModificarEstado(etlCliente cli, long USUARIO){
            try{
                bool MODIFICADO = false;
                using (var contextoBD = new ARMEntities()){
                    var CLIENTE = contextoBD.Clientes.SingleOrDefault(b => b.clienteId == cli.ID_Cliente);
                    string VIEJOS = "Provincia: " + CLIENTE.provinciaId + ", Nombre: " + CLIENTE.clienteNombre + ", Correo: " + CLIENTE.clienteCorreo +
                             ", Estado: " + CLIENTE.clienteEstado;

                    if (CLIENTE != null){

                        if (CLIENTE.clienteEstado == "Activo"){
                            CLIENTE.clienteEstado = "Inactivo";
                        }else{
                            CLIENTE.clienteEstado = "Activo";
                        }
                        contextoBD.SaveChanges();
                        MODIFICADO = true;

                    }
                    string NUEVOS = "Provincia: " + CLIENTE.provinciaId + ", Nombre: " + CLIENTE.clienteNombre + ", Correo: " + CLIENTE.clienteCorreo +
                           ", Estado: " + CLIENTE.clienteEstado;
                    var ACCION = "Modificación del estado de un Cliente";
                    GuardarEnBitacora(USUARIO, ACCION, VIEJOS, NUEVOS);
                }
                return MODIFICADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ModificarEstado

        public List<SelectListItem> ConsultarClientesActivos(){
            try{
                using (var contextoBD = new ARMEntities()){

                    var result = (from x in contextoBD.Clientes where x.clienteEstado == "Activo" select x).ToList();

                    var itemLista = (from item in result select new SelectListItem { Value = item.clienteId.ToString(), Text = item.clienteId.ToString()+" - "+item.clienteNombre }).ToList();

                    List<SelectListItem> ListaClientes = new List<SelectListItem>();

                    ListaClientes.AddRange(itemLista);

                    return ListaClientes.ToList();
                }
            }catch (Exception e){
                throw new System.Exception("");
            }
        }//FIN DE ConsultarClientes
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

    }//FIN DE ClienteModelo
}