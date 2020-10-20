namespace ProyectoProgramacion.ETL
{
    public class etlCanton
    {
        //ID Canton
        public long ID_Canton { get; set; }
        //Canton
        public string Canton { get; set; }
        //Provincia
        public etlProvincia Provincia { get; set; }

    }
}