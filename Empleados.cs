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
    
    public partial class Empleados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleados()
        {
            this.Solicitudes = new HashSet<Solicitudes>();
        }
    
        public Nullable<int> TipoId { get; set; }
        public long empleadoCedula { get; set; }
        public string empleadoNombre { get; set; }
        public string empleadoPrimerA { get; set; }
        public string empleadoSegundoA { get; set; }
        public string empleadoCorreo { get; set; }
        public string empleadoEstado { get; set; }
    
        public virtual TipoCedula TipoCedula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
