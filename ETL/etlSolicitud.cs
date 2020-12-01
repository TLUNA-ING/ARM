using System;
using System.ComponentModel.DataAnnotations;


namespace ProyectoProgramacion.ETL
{
    public class etlSolicitud

    {

        [Key]
        public int ID_Solicitud { get; set; } = 0;

        public etlCliente Cliente { get; set; } = new etlCliente();

        public etlProvincia Provincia { get; set; } = new etlProvincia();

        public etlDepartamento Departamento { get; set; } = new etlDepartamento();

        public etlTipoTrabajo TipoTrabajo { get; set; } = new etlTipoTrabajo();

        public etlEmpleado Empleado { get; set; } = new etlEmpleado();

        public etlEquipo Equipo { get; set; } = new etlEquipo();

        public string Fecha_Reporte { get; set; }


        public string horaEntrada { get; set; }
        
        public string horaSalida { get; set; }

        public string tipoHora { get; set; }

        public string cantidadHoras { get; set; }

        public string solicitudMotivo { get; set; }

        public string motivoDetalle { get; set; }

        public string solicitudRepuestos { get; set; }

        public long equipoDetenido { get; set; }

        public Nullable<System.TimeSpan> tiempoDetenido { get; set; }
        public string correoMQC { get; set; }
        public string nombreMQC { get; set; }
        public string cedulaMQC { get; set; }


        public string firmaCliente { get; set; }


    }
}