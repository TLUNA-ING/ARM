using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoProgramacion.ETL
{
    public class etlSeguridad{

        public string Encriptar(string Valor){
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(Valor);
            result = Convert.ToBase64String(encryted);
            return result;
        }//FIN DE Encriptar

        public string DesEncriptar(string Valor){
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(Valor);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }//FIN DE etlSeguridad
}