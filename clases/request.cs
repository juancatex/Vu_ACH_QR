using System.Net;
using System.Runtime.Serialization;

namespace Vu_ACH_QR.clases
{
    public class ResponseToken : response
    {
        [DataMember]
        public string SessionToken { get; set; } 
    }
    public class ResponseVu : response
    {
        [DataMember]
        public int Cerror { get; set; }
        public string Merror { get; set; }
    }
    public class RequestToken : request
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
    }
    public class Requestbt : request
    {
        public int Tiempo { get; set; } 
    }
    public class request
    {
        [DataMember]
        public Btinreq Btinreq { get; set; }
    }
    public class response
    {
        [DataMember]
        public Btinreq Btinreq { get; set; }
        [DataMember]
        public Erroresnegocio Erroresnegocio { get; set; }
        [DataMember] 
        public Btoutreq Btoutreq { get; set; }
        public response() {
        
        }
    }
    public class Btinreq
    {
        [DataMember]
        public string Device { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Requerimiento { get; set; }
        [DataMember]
        public string Canal { get; set; }
        [DataMember]
        public string Token { get; set; }

        public Btinreq() {
            Device = ValidarIP();
        }
        public Btinreq(String ip)
        {
            Device = ValidarIP(ip);
        }
        public Btinreq(String ip, String ipref)
        {
            Device = ValidarIP(ip, ipref);
        }
        protected String ValidarIP(String ipin="", String ipref = "")
        {
            IPAddress ipdefault = new IPAddress(new byte[] { 0, 0, 0, 0 });
            IPAddress? outip;
            if (ipin != ""&& ipref == "")
            {
                IPAddress.TryParse(ipin.Trim(), out outip);
                return outip != null ? outip.ToString() : ipdefault.ToString();
            }
            else { 
                IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress? ipAddress = heserver.AddressList.ToList().Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                && p.ToString().Trim().StartsWith(ipref != "" ? ipref : "1.")).FirstOrDefault(); 
                IPAddress.TryParse(ipAddress != null ? ipAddress.ToString() : (ipin != "" ? ipin : ipdefault.ToString()), out outip);
                return outip != null ? outip.ToString() : (ipin != "" ? ipin : ipdefault.ToString());
            }
        }
    }
    
     
    public class Btoutreq
    {
        [DataMember]
        public string Canal { get; set; }
        [DataMember]
        public string Servicio { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public string Hora { get; set; }
        [DataMember]
        public string Requerimiento { get; set; }
        [DataMember]
        public int Numero { get; set; }
        [DataMember]
        public string Estado { get; set; }
    }

    public class Erroresnegocio
    {
        [DataMember]
        public List<detailErroresnegocio> BTErrorNegocio { get; set; } 
    }

    public class detailErroresnegocio
    {
        [DataMember]
        public int Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Severidad { get; set; }
     
    }

}
