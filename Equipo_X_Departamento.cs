//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoProgramacion
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipo_X_Departamento
    {
        public int departamentoId { get; set; }
        public int equipoId { get; set; }
        public int cant_equipos { get; set; }
    
        public virtual Departamentos Departamentos { get; set; }
        public virtual Equipos Equipos { get; set; }
    }
}
