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
    
    public partial class Bitacora
    {
        public int bitacoraId { get; set; }
        public Nullable<long> usuarioId { get; set; }
        public Nullable<System.DateTime> fechaHora { get; set; }
        public string accionRealizada { get; set; }
        public string valoresViejos { get; set; }
        public string valoresNuevos { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}