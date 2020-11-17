using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoProgramacion.ETL
{
    public class etlSeguridad{

        public string Encriptar(string texto){
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(texto);
            result = Convert.ToBase64String(encryted);
            return result;
        }//FIN DE Encriptar

        public string DesEncriptar(string texto){
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(texto);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }//FIN DE DesEncriptar
}