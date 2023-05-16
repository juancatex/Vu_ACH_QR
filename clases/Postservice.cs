using RestSharp;
using RestSharp.Authenticators;
using System.Net; 

namespace Vu_ACH_QR.clases
{
    internal class Postservice
    {
        protected string _localDomain;
        protected string _userntlm;
        protected string _passntlm;
        protected string _body;
        protected int _timeoutjob;

        public Postservice(String user, String pass, String body,int tim)
        {
            _userntlm = user;
            _passntlm = pass;
            _body = body;
            _localDomain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            _timeoutjob = tim;
        }
         
        public RestResponse getpostDataNTLM(String url) {
            var credentials = new NetworkCredential(_userntlm, _passntlm, _localDomain);
            RestClientOptions options = new RestClientOptions(url) { UseDefaultCredentials = false, Credentials = credentials, MaxTimeout = _timeoutjob };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", _body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            client.Dispose(); 
            return response;
        }
        public RestResponse getpostDataBasic(String url)
        {
            HttpBasicAuthenticator authBasic = new HttpBasicAuthenticator(_userntlm, _passntlm);
            RestClientOptions options = new RestClientOptions(url) { Authenticator = authBasic, MaxTimeout = _timeoutjob }; 
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", _body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            client.Dispose();
            return response;
        }
    }
}
