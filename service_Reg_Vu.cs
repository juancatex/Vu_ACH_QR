using Quartz;
using RestSharp;
using System.Drawing;
using System.Net;
using Vu_ACH_QR.clases;

namespace Vu_ACH_QR
{
    internal class service_Reg_Vu : IJob
    { 
        private readonly Config_quartz _dataconfig; 
        private readonly ILogger<service_Reg_Vu> _logger;
        private readonly Log _reglog;
        private string _trasaccion;
        public service_Reg_Vu(ILogger<service_Reg_Vu> logger, Config_quartz options)
        {
            _logger = logger;
            _dataconfig = options;
            _reglog = new Log(options.LogtxtPath, "Vu_ACH_QR");
        }
        private RestResponse gettokken() {
            RequestToken jsontoken = new RequestToken()
            {
                Btinreq = new Btinreq(_dataconfig.Device)
                {
                    Usuario = _dataconfig.Usuario,
                    Requerimiento = _dataconfig.Requerimiento,
                    Canal = _dataconfig.Canal,
                    Token = "null"
                },
                UserId = _dataconfig.Usuario,
                UserPassword = _dataconfig.Password
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(jsontoken);
            Postservice serv = new Postservice(_dataconfig.UsuarioDOM, _dataconfig.PasswordDOM, jsonString, _dataconfig.TimeoutJob);
            return serv.getpostDataNTLM(_dataconfig.UrlToken);
            //return serv.getpostDataBasic(_dataconfig.UrlToken);
        }
        private RestResponse regVu(String tokken)
        {
            Requestbt jsontoken = new Requestbt()
            {
                Btinreq = new Btinreq(_dataconfig.Device)
                {
                    Usuario = _dataconfig.Usuario,
                    Requerimiento = _dataconfig.Requerimiento,
                    Canal = _dataconfig.Canal,
                    Token = tokken
                },
                Tiempo = 10
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(jsontoken);
          
            Postservice serv = new Postservice(_dataconfig.UsuarioDOM, _dataconfig.PasswordDOM, jsonString, _dataconfig.TimeoutJob);
            return serv.getpostDataNTLM(_dataconfig.Urlbt);
           // return serv.getpostDataBasic(_dataconfig.Urlbt);
        }
        private void validarVu(string tokken) {
            RestResponse response = regVu(tokken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseVu datarequest = System.Text.Json.JsonSerializer.Deserialize<ResponseVu>(response.Content);
                 
                switch (datarequest.Btoutreq.Estado)
                {
                    case "OK":
                        _logger.LogInformation("Ok----{3}-----{2}--->>>>>>>>>>>----   {0}-------------------{1}", datarequest.Cerror, datarequest.Merror, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), datarequest.Btinreq.Device);
                        _reglog.GrabaLogHora(datarequest.Btinreq.Device + "|" + this._trasaccion + ":" + "OK, Code=" + datarequest.Btoutreq.Estado + " Servicio=" 
                            + datarequest.Btoutreq.Servicio+" CodeResult="+ datarequest.Cerror+" MensajeResult="+ datarequest.Merror);
                        break;
                    default:
                        _logger.LogInformation("--{1}--------------------{0}", datarequest.Erroresnegocio.BTErrorNegocio[0].Descripcion, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                        _reglog.GrabaLogHora(datarequest.Btinreq.Device + "|" + this._trasaccion + ":" + "Error con el programa Btservice, Code=" + datarequest.Erroresnegocio.BTErrorNegocio[0].Codigo + " Mensaje=" + datarequest.Erroresnegocio.BTErrorNegocio[0].Descripcion);
                        break;
                }
            } else
            {
                _logger.LogInformation("--{0}--------------------{1}", response.Content, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " ErrorMessage=" + response.ErrorMessage); 
                _reglog.GrabaLogHora(this._trasaccion + ":" + "Error en la conexión con el Btservice, Code=" + response.StatusCode + " Mensaje=" + response.Content + " ErrorMessage=" + response.ErrorMessage);
            }
        }
        
       public Task Execute(IJobExecutionContext context)
        { 
            try{ 
                    this._trasaccion = _reglog.getKey(10);
                    _logger.LogInformation("Inicio envio Vu :   {0}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    _reglog.GrabaLogHora(this._trasaccion+":"+"Inicio envio Vu.");
                    RestResponse response = gettokken();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ResponseToken datarequest=System.Text.Json.JsonSerializer.Deserialize<ResponseToken>(response.Content);
                        switch (datarequest.Btoutreq.Estado)
                        {
                            case "OK":
                                validarVu(datarequest.SessionToken);
                                break;
                            default:
                                _logger.LogInformation("{2}----{1}--------------------{0}", datarequest.Erroresnegocio.BTErrorNegocio[0].Descripcion, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), datarequest.Btinreq.Device);
                                _reglog.GrabaLogHora(datarequest.Btinreq.Device + "|"+this._trasaccion + ":" + "Error al validar el TOKEN, Code=" + datarequest.Erroresnegocio.BTErrorNegocio[0].Codigo + " Mensaje=" + datarequest.Erroresnegocio.BTErrorNegocio[0].Descripcion);
                                break;
                        }
                    }else{
                        _logger.LogInformation("--{0}---------tokken----------------{1}-----------Msg:{2}", response.StatusCode, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), response.Content + " ErrorMessage=" + response.ErrorMessage);
                        _reglog.GrabaLogHora(this._trasaccion + ":" + "Error al obtener el TOKEN, Code=" + response.StatusCode + " Mensaje="+ response.Content + " ErrorMessage=" + response.ErrorMessage);
                    }
            }catch (Exception e){
                _logger.LogInformation("-----btservices error----------------{0}-----------Msg:{1}" , DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), e.Message); 
                _reglog.GrabaLogHora(this._trasaccion + ":" + "Error de conexion con el BTservices, Mensaje=" + e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
