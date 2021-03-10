using ProyectoProgramacion.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;

namespace ProyectoProgramacion
{
    public class MvcApplication : System.Web.HttpApplication{
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Session_End(object sender, EventArgs e){
        //    //var routeData = new RouteData();
        //    //routeData.Values["controller"] = "Home";
        //    //routeData.Values["action"] = "Logout";
        //    //HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
        //    //IController controller = new HomeController();
        //    //var rc = new RequestContext(currentContext, routeData);
        //    //controller.Execute(rc);
        //    Response.Redirect("~/Home/Logout");
        //}
    }
}
