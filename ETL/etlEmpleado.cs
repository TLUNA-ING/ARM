namespace ProyectoProgramacion.ETL
{
    public class etlEmpleado
    {
        //Cedula
        public long Cedula { get; set; }
        //Nombre
        public string Nombre { get; set; }

        //Primer apellido
        public string Primer_Apellido { get; set; }
        //Segundo Apellido
        public string Segundo_Apellido { get; set; }
        //Telefono
        public string Telefono { get; set; }
        //Correo
        public string Correo { get; set; }
        //Direccion
        public etlTipoTrabajo Direccion { get; set; }

    }
}