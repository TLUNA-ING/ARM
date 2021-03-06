﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ARMEntities : DbContext
    {
        public ARMEntities()
            : base("name=ARMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<CodigoRecuperacion> CodigoRecuperacion { get; set; }
        public virtual DbSet<Departamento_X_Cliente> Departamento_X_Cliente { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Equipo_X_Departamento> Equipo_X_Departamento { get; set; }
        public virtual DbSet<Equipos> Equipos { get; set; }
        public virtual DbSet<InfoSMTP> InfoSMTP { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Solicitudes> Solicitudes { get; set; }
        public virtual DbSet<TipoCedula> TipoCedula { get; set; }
        public virtual DbSet<TipoTrabajo> TipoTrabajo { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    
        public virtual ObjectResult<CONSULTAR_EMPLEADOS_NO_USUARIO_Result> CONSULTAR_EMPLEADOS_NO_USUARIO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CONSULTAR_EMPLEADOS_NO_USUARIO_Result>("CONSULTAR_EMPLEADOS_NO_USUARIO");
        }
    
        public virtual ObjectResult<CONSULTAR_INFORMACION_INTERMEDIA_Result> CONSULTAR_INFORMACION_INTERMEDIA(string tABLA, Nullable<int> iD, string iND_LIGADOS)
        {
            var tABLAParameter = tABLA != null ?
                new ObjectParameter("TABLA", tABLA) :
                new ObjectParameter("TABLA", typeof(string));
    
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var iND_LIGADOSParameter = iND_LIGADOS != null ?
                new ObjectParameter("IND_LIGADOS", iND_LIGADOS) :
                new ObjectParameter("IND_LIGADOS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CONSULTAR_INFORMACION_INTERMEDIA_Result>("CONSULTAR_INFORMACION_INTERMEDIA", tABLAParameter, iDParameter, iND_LIGADOSParameter);
        }
    
        public virtual ObjectResult<CONSULTAR_UN_EMPLEADO_BD_Result> CONSULTAR_UN_EMPLEADO_BD(string cEDULA)
        {
            var cEDULAParameter = cEDULA != null ?
                new ObjectParameter("CEDULA", cEDULA) :
                new ObjectParameter("CEDULA", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CONSULTAR_UN_EMPLEADO_BD_Result>("CONSULTAR_UN_EMPLEADO_BD", cEDULAParameter);
        }
    
        public virtual ObjectResult<InformacionClientes_Result> InformacionClientes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InformacionClientes_Result>("InformacionClientes");
        }
    
        public virtual ObjectResult<InformacionEmpleados_Result> InformacionEmpleados()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InformacionEmpleados_Result>("InformacionEmpleados");
        }
    
        public virtual int INSERTAR_CODIGO_USUARIO_RECUPERACION(string uSUARIO, string cODIGO)
        {
            var uSUARIOParameter = uSUARIO != null ?
                new ObjectParameter("USUARIO", uSUARIO) :
                new ObjectParameter("USUARIO", typeof(string));
    
            var cODIGOParameter = cODIGO != null ?
                new ObjectParameter("CODIGO", cODIGO) :
                new ObjectParameter("CODIGO", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERTAR_CODIGO_USUARIO_RECUPERACION", uSUARIOParameter, cODIGOParameter);
        }
    
        public virtual int INSERTAR_EN_BITACORA(Nullable<long> uSUARIO, string aCCION, string vIEJOS, string nUEVOS)
        {
            var uSUARIOParameter = uSUARIO.HasValue ?
                new ObjectParameter("USUARIO", uSUARIO) :
                new ObjectParameter("USUARIO", typeof(long));
    
            var aCCIONParameter = aCCION != null ?
                new ObjectParameter("ACCION", aCCION) :
                new ObjectParameter("ACCION", typeof(string));
    
            var vIEJOSParameter = vIEJOS != null ?
                new ObjectParameter("VIEJOS", vIEJOS) :
                new ObjectParameter("VIEJOS", typeof(string));
    
            var nUEVOSParameter = nUEVOS != null ?
                new ObjectParameter("NUEVOS", nUEVOS) :
                new ObjectParameter("NUEVOS", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("INSERTAR_EN_BITACORA", uSUARIOParameter, aCCIONParameter, vIEJOSParameter, nUEVOSParameter);
        }
    
        public virtual ObjectResult<solicitudesPorRangoFechas_Result> solicitudesPorRangoFechas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<solicitudesPorRangoFechas_Result>("solicitudesPorRangoFechas");
        }
    
        public virtual ObjectResult<SPDetalleSolicitud_Result> SPDetalleSolicitud(Nullable<int> solicitudID)
        {
            var solicitudIDParameter = solicitudID.HasValue ?
                new ObjectParameter("solicitudID", solicitudID) :
                new ObjectParameter("solicitudID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPDetalleSolicitud_Result>("SPDetalleSolicitud", solicitudIDParameter);
        }
    
        public virtual ObjectResult<SPsolicitudes_Result> SPsolicitudes(Nullable<int> solicitudID)
        {
            var solicitudIDParameter = solicitudID.HasValue ?
                new ObjectParameter("solicitudID", solicitudID) :
                new ObjectParameter("solicitudID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPsolicitudes_Result>("SPsolicitudes", solicitudIDParameter);
        }
    }
}
