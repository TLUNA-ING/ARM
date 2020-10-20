using ProyectoProgramacion.Controllers;
using ProyectoProgramacion.ETL;
using System;
using System.Web;
using System.Web.Mvc;

namespace ProyectoProgramacion.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        private etlUsuario oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (etlUsuario)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Index");
                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Index");
            }

        }
    }
}