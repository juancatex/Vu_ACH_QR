using RestSharp;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Vu_ACH_QR.clases
{
    internal class Encript
    {
         readonly Config_quartz _datain;

        public Encript(Config_quartz valuein) {
            _datain = valuein;
        }
        public Config_quartz getParameters()
        {
            string sNoSerie = "";
            switch (_datain.ValCer)
            {  
                case "Server":
                        // crear la instance de llamado al certificado instalado en el server
                        X509Store objStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                        objStore.Open(OpenFlags.ReadOnly);
                        foreach (X509Certificate2 objCert in objStore.Certificates)
                        {
                            string cn = objCert.IssuerName.Name + ""; 
                            if (cn == "CN=BSOL-VADLPZSRV-CA, DC=bsol, DC=com, DC=bo")
                            {
                                sNoSerie = objCert.SerialNumber.ToString();
                            }
                        }
                        objStore.Close();
                    break;
                case "File":
                        // crear la instance de llamado al certificado instalado en un repositorio
                        byte[] yCert = File.ReadAllBytes(_datain.PathCer);
                        X509Certificate2 oCSD = new X509Certificate2(yCert);
                        sNoSerie = oCSD.SerialNumber.ToString();
                    break;
            }
          
            Criptografia criptografia = new Criptografia(_datain.UrlKeys, sNoSerie); 
            Config_quartz outconfig = new Config_quartz();
            outconfig = _datain;
            outconfig.Usuario = criptografia.Desencriptar(_datain.Usuario);
            outconfig.Password = criptografia.Desencriptar(_datain.Password);
            outconfig.UsuarioDOM = criptografia.Desencriptar(_datain.UsuarioDOM);
            outconfig.PasswordDOM = criptografia.Desencriptar(_datain.PasswordDOM); 
            outconfig.PathCer = "";
            outconfig.UrlKeys = "";

            return outconfig;
        }
    }
}
