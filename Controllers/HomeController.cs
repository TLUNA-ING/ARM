using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class HomeController : Controller{
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Logout(){
            Session["User"] = null;
            return RedirectToAction("Index", "Acceso");
        }

        [HttpPost]
        public ActionResult ValidarRefrescar(){
            try{
                if(Session["User"] == null){
                    return Json("SI", JsonRequestBehavior.AllowGet);
                }else{
                    return Json("NO", JsonRequestBehavior.AllowGet);
                }
            }catch (Exception e){
                return Json("SI", JsonRequestBehavior.AllowGet);
            }
        }//FIN DE ValidarRefrescar

    }
}