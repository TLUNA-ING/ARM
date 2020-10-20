namespace ProyectoProgramacion.Controllers
{
    //public class SeguimientoController : Controller
    //{
    //    // GET: Seguimiento
    //    [AutorizarUsuario(rol: "admin,user")]
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }
    //    [AutorizarUsuario(rol: "admin,user")]
    //    public ActionResult CargarDatos(string ID)
    //    {
    //        var respuesta = new SolicitudSeguimientoModelo().ConsultarTodos(ID);
    //        if (respuesta == null)
    //        {
    //            return Json(respuesta, JsonRequestBehavior.DenyGet);
    //        }
    //        else
    //        {
    //            return Json(respuesta, JsonRequestBehavior.AllowGet);
    //        }
    //    }


    //    //Agregar datos
    //    [AutorizarUsuario(rol: "admin,user")]
    //    [HttpPost]
    //    public ActionResult Agregar(etlSeguimiento sol)
    //    {
    //        try
    //        {
    //            new SolicitudSeguimientoModelo().GuardarConsulta(sol);
    //            return Json(sol, JsonRequestBehavior.AllowGet);
    //        }
    //        catch (Exception e)
    //        {
    //            return Json(e, JsonRequestBehavior.DenyGet);
    //        }


    //    }

    //    [HttpPost]
    //    [AutorizarUsuario(rol: "admin,user")]
    //    public ActionResult Eliminar(string id)
    //    {
    //        try
    //        {
    //            new SolicitudSeguimientoModelo().Eliminar(id);
    //            return Json(id, JsonRequestBehavior.AllowGet);
    //        }
    //        catch (Exception e)
    //        {
    //            return Json(e, JsonRequestBehavior.DenyGet);
    //        }


    //    }
    //    [AutorizarUsuario(rol: "admin,user")]
    //    public ActionResult Consultar(string id)
    //    {
    //        try
    //        {
    //            var respuesta = new SolicitudSeguimientoModelo().Consultar(id);
    //            return Json(respuesta, JsonRequestBehavior.AllowGet);
    //        }
    //        catch (Exception e)
    //        {
    //            return Json(e, JsonRequestBehavior.DenyGet);
    //        }


    //    }
    //    [AutorizarUsuario(rol: "admin,user")]
    //    [HttpPost]
    //    public ActionResult Actualizar(etlSeguimiento sol)
    //    {
    //        try
    //        {
    //            new SolicitudSeguimientoModelo().Actualizar(sol);
    //            return Json(sol, JsonRequestBehavior.AllowGet);
    //        }
    //        catch (Exception e)
    //        {
    //            return Json(e, JsonRequestBehavior.DenyGet);
    //        }


    //    }
    //}
}