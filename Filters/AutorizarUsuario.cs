using ProyectoProgramacion.ETL;
using System;
using System.Web;
using System.Web.Mvc;

namespace ProyectoProgramacion.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutorizarUsuario : AuthorizeAttribute
    {
        private string[] rol;
        private etlUsuario usuario;
        public AutorizarUsuario(string rol)
        {
            this.rol = rol.Split(',');
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool valido = false;
            try
            {
                usuario = (etlUsuario)HttpContext.Current.Session["User"];
                if (usuario != null)
                {
                    foreach (String s in rol)
                    {
                        if (s == usuario.Rol.Rol)
                        {
                            valido = true;
                        }
                    }
                }

                if (!valido) { filterContext.Result = new RedirectResult("~/Home/Error"); }

            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Home/Error");
            }
        }

    }
}