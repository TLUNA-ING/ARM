namespace ProyectoProgramacion.ETL
{
    public class etlCliente
    {
        public int ID_Cliente { get; set; } = 0;

        public string Descripcion { get; set; } = "";

        public etlProvincia Provincia { get; set; } = new etlProvincia();
    }
}