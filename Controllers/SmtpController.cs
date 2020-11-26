using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Filters;
using ProyectoProgramacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class SmtpController : Controller{

        [AutorizarUsuario(rol: "admin")]
        public ActionResult CargarSMTP(){
            SmtpModelo modelSmtp = new SmtpModelo();
            var SMTP = modelSmtp.ConsultarSmtp();

            if (SMTP == null) {
                return Json(SMTP, JsonRequestBehavior.DenyGet);
            } else{
                return Json(SMTP, JsonRequestBehavior.AllowGet);
            }
        }//FIN DE CargarSMTP

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult ProbarSmtp(etlSmtp smtp){
            try{
                SmtpModelo modelSmtp = new SmtpModelo();

                var ENVIADO = modelSmtp.ProbarSmtp(smtp);

                if (ENVIADO == true){
                return Json("Enviado", JsonRequestBehavior.AllowGet);
                }else{
                return Json("No Enviado", JsonRequestBehavior.AllowGet);
                }  
                   
            }catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE ProbarSmtp

        [AutorizarUsuario(rol: "admin")]
        [HttpPost]
        public ActionResult AgregarSmtp(etlSmtp smtp){
            try{
                SmtpModelo modelSmtp = new SmtpModelo();
                var ENVIADO = modelSmtp.ProbarSmtp(smtp);

                if (ENVIADO == true) {
                    var AGREGADO = modelSmtp.AgregarSmtp(smtp);

                    if (AGREGADO == true){
                        return Json("Agregado", JsonRequestBehavior.AllowGet);
                    }else{
                        return Json("666", JsonRequestBehavior.AllowGet);
                    }
                }else{
                    return Json("No Enviado", JsonRequestBehavior.AllowGet);
                }
            } catch (Exception e){
                return Json(e, JsonRequestBehavior.DenyGet);
            }
        }//FIN DE AgregarSmtp

    }//FIN DE SmtpController
}