
using Seguridad;
using System.Security.Cryptography.X509Certificates; 

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
            Config_quartz outconfig = new Config_quartz();
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
                    Criptografia criptografia = new Criptografia(_datain.UrlKeys, sNoSerie);

                    outconfig = _datain;
                    outconfig.Usuario = criptografia.Desencriptar(_datain.Usuario);
                    outconfig.Password = criptografia.Desencriptar(_datain.Password);
                    outconfig.UsuarioDOM = criptografia.Desencriptar(_datain.UsuarioDOM);
                    outconfig.PasswordDOM = criptografia.Desencriptar(_datain.PasswordDOM);
                    outconfig.PathCer = "";
                    outconfig.UrlKeys = "";
                    break;
                case "File":
                        // crear la instance de llamado al certificado instalado en un repositorio
                        byte[] yCert = File.ReadAllBytes(_datain.PathCer);
                        X509Certificate2 oCSD = new X509Certificate2(yCert);
                        sNoSerie = oCSD.SerialNumber.ToString();
                    Criptografia criptografiaf = new Criptografia(_datain.UrlKeys, sNoSerie);

                    outconfig = _datain;
                    outconfig.Usuario = criptografiaf.Desencriptar(_datain.Usuario);
                    outconfig.Password = criptografiaf.Desencriptar(_datain.Password);
                    outconfig.UsuarioDOM = criptografiaf.Desencriptar(_datain.UsuarioDOM);
                    outconfig.PasswordDOM = criptografiaf.Desencriptar(_datain.PasswordDOM);
                    outconfig.PathCer = "";
                    outconfig.UrlKeys = "";
                    break;
                case "Line":
                    // crear la instance de llamado al certificado fisico ()obtenido desde el dll
                    byte[] yCertoff = File.ReadAllBytes(_datain.PathCer);
                    X509Certificate2 oCSDoff = new X509Certificate2(yCertoff);
                    sNoSerie = oCSDoff.SerialNumber.ToString();
                    Cripto Cripto = new Cripto(_datain.UrlKeys, sNoSerie);

                    outconfig = _datain;
                    outconfig.Usuario = Cripto.Desencriptar(_datain.Usuario);
                    outconfig.Password = Cripto.Desencriptar(_datain.Password);
                    outconfig.UsuarioDOM = Cripto.Desencriptar(_datain.UsuarioDOM);
                    outconfig.PasswordDOM = Cripto.Desencriptar(_datain.PasswordDOM);
                    outconfig.PathCer = "";
                    outconfig.UrlKeys = "";
                    break;
                default:
                    outconfig = _datain;
                    break;
            } 
            return outconfig;
        }
    }
}
