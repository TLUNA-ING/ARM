using ProyectoProgramacion.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ProyectoProgramacion.Models
{
    public class SmtpModelo{

        List<InfoSMTP> SMTP = new List<InfoSMTP>();
        public etlSmtp ConsultarSmtp(){
            etlSeguridad seguridad = new etlSeguridad();
            etlSmtp configuracion = new etlSmtp();
            using (var contextoBD = new ARMEntities()){
                SMTP = (from x in contextoBD.InfoSMTP select x).ToList();
                foreach (var SM in SMTP){

                    configuracion.Correo = SM.Correo;
                    configuracion.Password = seguridad.DesEncriptar(SM.Password);
                    configuracion.ServidorSMTP = SM.ServidorSMTP;
                    configuracion.Puerto = SM.Puerto;
                    configuracion.SSL = SM.SSL;
                }
            }
            return configuracion;
        } //FIN DE ConsultarSMTP

        public bool ProbarSmtp(etlSmtp smtp){
            try{
                bool ENVIADO = false;

                SmtpClient client = new SmtpClient(smtp.ServidorSMTP, smtp.Puerto);
                if (smtp.SSL=="1"){
                    client.EnableSsl = true;
                }else{
                    client.EnableSsl = false;
                }
                client.Timeout = 30000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtp.Correo, smtp.Password);
                MailMessage email = new MailMessage(smtp.Correo,smtp.Correo,"Pruebas de SMTP","Esto es una prueba automática, por favor no responder este correo.");
                email.IsBodyHtml = true;
                email.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(email);

                ENVIADO = true;

                return ENVIADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE ProbarSmtp

        public bool AgregarSmtp(etlSmtp smtp){
            try{
                bool AGREGADO = false;
                etlSeguridad seguridad = new etlSeguridad();
                using (var contextoBD = new ARMEntities()){

                    var result = (from x in contextoBD.InfoSMTP select x).ToList();
                    contextoBD.InfoSMTP.RemoveRange(result);
                    contextoBD.SaveChanges();

                    InfoSMTP item = new InfoSMTP();
                    item.Correo = smtp.Correo;
                    item.Password = seguridad.Encriptar(smtp.Password);
                    item.ServidorSMTP = smtp.ServidorSMTP;
                    item.Puerto = smtp.Puerto;

                    if (smtp.SSL == "1"){
                        item.SSL = "S";
                    }else{
                        item.SSL = "N";
                    }

                    contextoBD.InfoSMTP.Add(item);
                    contextoBD.SaveChanges();
                    AGREGADO = true;
                }

                return AGREGADO;
            }catch (Exception e){
                return false;
            }
        }//FIN DE AgregarSmtp
    }
}