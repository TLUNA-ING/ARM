namespace ProyectoProgramacion.ETL
{
    public class etlUsuario{
        public string Password { get; set; } = "";
        public etlEmpleado Empleado { get; set; } = new etlEmpleado();
        public etlRol Rol { get; set; } = new etlRol();
    }
}