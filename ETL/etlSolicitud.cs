using System;

namespace ProyectoProgramacion.ETL
{
    public class etlSolicitud
    {
        public int ID_Solicitud { get; set; } = 0;

        public etlCliente Cliente { get; set; } = new etlCliente();

        public etlDepartamento Departamento { get; set; } = new etlDepartamento();

        public string Clientes { get; set; } = "";

        public long Cedula { get; set; } = 0;

        public DateTime Fecha_Reporte { get; set; }
        public String Fecha { get; set; } = "";

        public string Reporte { get; set; } = "";
    }
}