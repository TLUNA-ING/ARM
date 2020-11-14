namespace ProyectoProgramacion.ETL
{
    public class etlCliente
    {
        public int ID_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Estado { get; set; }
        public etlProvincia Provincia { get; set; } = new etlProvincia();
    }
}