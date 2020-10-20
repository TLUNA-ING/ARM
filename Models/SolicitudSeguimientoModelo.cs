namespace ProyectoProgramacion.Models
{
    //public class SolicitudSeguimientoModelo
    // {
    //    public List<etlSeguimiento> ConsultarTodos(string ID)
    //    {
    //        long l_ID = long.Parse(ID);
    //        using (var contextoBD = new ARMEntities())
    //        {

    //            List<SeguimientoSolicitud> ss = new List<SeguimientoSolicitud>();
    //            ss= (from d in contextoBD.SeguimientoSolicitud
    //             where d.ID_Solicitud == l_ID
    //                 select d).ToList();

    //            List<etlSeguimiento> respuesta = new List<etlSeguimiento>();

    //            foreach (SeguimientoSolicitud x in ss)
    //            {
    //                respuesta.Add(new etlSeguimiento
    //                {
    //                    Fecha= x.Fecha_Laborada.ToString(),
    //                    ID_SeguimientoSolicitud= x.ID_SeguimientoSolicitud,
    //                    Fecha_Laborada = x.Fecha_Laborada,
    //                    HorasDoble = (int)x.HoraDoble,
    //                    HorasExtras = (int)x.HoraExtras,
    //                    HorasNormales = (int)x.HoraNormales,
    //                    TrabajoRealizado = x.TrabajoRealizado.Trim(),
    //                    Solicitud = new etlSolicitud
    //                    {
    //                        ID_Solicitud=x.Solicitud.ID_Solicitud
    //                    }

    //                });
    //            }
    //            return respuesta;
    //        }

    //    }

    //    public void GuardarConsulta(etlSeguimiento sol)
    //    {
    //        try
    //        {
    //            using (var contextoBD = new SOLICITUDESEntities())
    //            {
    //                SeguimientoSolicitud item = new SeguimientoSolicitud();
    //                item.ID_Solicitud = sol.Solicitud.ID_Solicitud;
    //                item.HoraNormales = sol.HorasNormales;
    //                item.HoraExtras = sol.HorasExtras;
    //                item.HoraDoble = sol.HorasDoble;
    //                item.Fecha_Laborada = sol.Fecha_Laborada;
    //                item.TrabajoRealizado = sol.TrabajoRealizado.Trim();
    //                contextoBD.SeguimientoSolicitud.Add(item);
    //                contextoBD.SaveChanges();
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new System.Exception("ID ya existe o hay datos sin llenar");
    //        }

    //    }
    //    public void Eliminar(string id)
    //    {
    //        try
    //        {
    //            using (var contextoBD = new SOLICITUDESEntities())
    //            {
    //                SeguimientoSolicitud sol = contextoBD.SeguimientoSolicitud.Find(long.Parse(id.Trim()));
    //                contextoBD.SeguimientoSolicitud.Remove(sol);
    //                contextoBD.SaveChanges();
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new System.Exception("Error al eliminar");
    //        }

    //    }

    //    public etlSeguimiento Consultar(string id)
    //    {
    //        try
    //        {
    //            using (var contextoBD = new SOLICITUDESEntities())
    //            {
    //                SeguimientoSolicitud x = contextoBD.SeguimientoSolicitud.Find(long.Parse(id.Trim()));
    //                etlSeguimiento solicitud = new etlSeguimiento
    //                {
    //                    Fecha = x.Fecha_Laborada.ToString(),
    //                    ID_SeguimientoSolicitud = x.ID_SeguimientoSolicitud,
    //                    Fecha_Laborada = x.Fecha_Laborada,
    //                    HorasDoble = (int)x.HoraDoble,
    //                    HorasExtras = (int)x.HoraExtras,
    //                    HorasNormales = (int)x.HoraNormales,
    //                    TrabajoRealizado = x.TrabajoRealizado.Trim(),
    //                    Solicitud = new etlSolicitud
    //                    {
    //                        ID_Solicitud = x.Solicitud.ID_Solicitud
    //                    }


    //                };
    //                solicitud.Fecha = solicitud.Fecha_Laborada.ToString("yyyy-MM-dd");
    //                return solicitud;
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new System.Exception("Error al eliminar");
    //        }

    //    }
    //    public void Actualizar(etlSeguimiento sol)
    //    {
    //        try
    //        {
    //            using (var contextoBD = new SOLICITUDESEntities())
    //            {
    //                SeguimientoSolicitud item = contextoBD.SeguimientoSolicitud.Find(sol.ID_SeguimientoSolicitud);

    //                item.HoraNormales = sol.HorasNormales;
    //                item.HoraExtras = sol.HorasExtras;
    //                item.HoraDoble = sol.HorasDoble;
    //                item.Fecha_Laborada = sol.Fecha_Laborada;
    //                item.TrabajoRealizado = sol.TrabajoRealizado.Trim();
    //                contextoBD.SaveChanges();
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new System.Exception("Error al actualizar");
    //        }

    //    }
    //}
}