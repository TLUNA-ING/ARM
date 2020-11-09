//using ProyectoProgramacion.ETL;
//using System.Collections.Generic;
//using System.Linq;

//namespace ProyectoProgramacion.Models
//{
//    public class SeguimientoModelo
//    {
//        public List<etlSolicitud> ConsultarTodos()
//        {
//            using (var contextoBD = new ARMEntities())
//            {
//                var respuesta = contextoBD.Solicitudes.Select(x =>
//                new etlSolicitud
//                {
//                    ID_Solicitud = x.solicitudId,
//                    Cliente = new etlCliente
//                    {
//                        ID_Cliente = x.Clientes.clienteId,
//                        Descripcion = x.Clientes.clienteNombre.Trim()
//                    },
//                    Departamento = new etlDepartamento
//                    {
//                        ID_Departamento = x.Departamentos.departamentoId,
//                        Descripcion = x.Departamentos.deparatamentoNombre.Trim()
//                    },
//                    Cedula = x.Empleados.empleadoCedula,
//                    Clientes = x.Clientes.clienteNombre.Trim(),
//                    Fecha_Reporte = x.fechaReporte,
//                    Reporte = x.solicitudMotivo.Trim(),
//                    Fecha = x.fechaReporte.ToString()

//                }).ToList();
//                return respuesta;
//            }

//        }
//    }
//}