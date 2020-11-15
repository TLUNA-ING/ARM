
using ProyectoProgramacion.Controllers;
using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoProgramacion.Models
{

    public class IntermedioModelo{

        List<etlIntermedio> Intermedio = new List<etlIntermedio>();
        public List<etlIntermedio> CargarDatosTablas(string TABLA, long ID, string IND_LIGADO){
            try{
                using (var contextoBD = new ARMEntities()){
                    int id = (int)ID;
                    var result = contextoBD.CONSULTAR_INFORMACION_INTERMEDIA(TABLA, id, IND_LIGADO);

                    foreach (var InterM in result){
                        etlIntermedio Inter = new etlIntermedio();
                        Inter.ID = InterM.ID;
                        Inter.Descripcion = InterM.DESCRIPCION;
                        Intermedio.Add(Inter);
                    }
                    return Intermedio;
                }
            }catch (Exception e){
                return Intermedio;
            }

        }//FIN DE ConsultarTodos

        List<Departamento_X_Cliente> Departamento_X_Cliente = new List<Departamento_X_Cliente>();
        public etlDepartamentoXCliente ConsultarIDs(long ID_CLIENTE, long ID_DEPARTAMENTO)
        {
            try
            {

                etlDepartamentoXCliente LIGA = new etlDepartamentoXCliente();
                using (var contextoBD = new ARMEntities())
                {

                    Departamento_X_Cliente = (from x in contextoBD.Departamento_X_Cliente where x.clienteId == ID_CLIENTE && x.departamentoId == ID_DEPARTAMENTO select x).ToList();
                    foreach (var L in Departamento_X_Cliente)
                    {
                        LIGA.ID_Departamento = L.departamentoId;
                        LIGA.ID_Cliente = L.clienteId;
                    }
                }
                return LIGA;
            }
            catch (Exception e)
            {
                throw new System.Exception("Error");
            }
        }//FIN DE ConsultarIDs

        public bool LigarDepartamentoIntermedio(etlDepartamentoXCliente Intermedio){
            try{
                bool AGREGADO = false;
                using (var contextoBD = new ARMEntities()){
                    Departamento_X_Cliente item = new Departamento_X_Cliente();

                    item.departamentoId = Intermedio.ID_Departamento;
                    item.clienteId = Intermedio.ID_Cliente;
                    item.cant_departamentos = 1;

                    contextoBD.Departamento_X_Cliente.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }
                return AGREGADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE LigarDepartamentoIntermedio

        public bool DesligarDepartamentoIntermedio(etlDepartamentoXCliente Intermedio)
        {
            try
            {
                bool DESLIGADO = false;
                using (var contextoBD = new ARMEntities())
                {
                    var resultado = contextoBD.Departamento_X_Cliente.SingleOrDefault(b => b.departamentoId == Intermedio.ID_Departamento && b.clienteId == Intermedio.ID_Cliente);
                    if (resultado != null)
                    {
                        contextoBD.Departamento_X_Cliente.Remove(resultado);
                        contextoBD.SaveChanges();
                        DESLIGADO = true;
                    }
                }
                return DESLIGADO;
            }
            catch (Exception e)
            {
                return false;
            }
        }//FIN DE DesligarDepartamentoIntermedio
    }
}