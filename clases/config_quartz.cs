using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vu_ACH_QR.clases
{
    internal class Config_quartz
    {
        public string CronJob { get; set; } 
        public string UrlToken { get; set; }
        public string Urlbt { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string UsuarioDOM { get; set; }
        public string PasswordDOM { get; set; }
        public string Requerimiento { get; set; } 
        public string Canal { get; set; }
        public string Deviceref { get; set; }
        public string Device { get; set; }
        public string LogtxtPath { get; set; }
        public int Hilos { get; set; }
        public int TimeoutJob { get; set; }
        public string? PathCer { get; set; }
        public string? UrlKeys { get; set; }
        public string ValCer { get; set; }

    }
}
