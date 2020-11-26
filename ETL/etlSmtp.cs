using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoProgramacion.ETL
{
    public class etlSmtp
    {
        public string Correo { get; set; }
        public string Password { get; set; }
        public string ServidorSMTP { get; set; }
        public int Puerto { get; set; }
        public string SSL { get; set; }
    }
}