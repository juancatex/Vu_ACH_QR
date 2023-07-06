using Microsoft.AspNetCore.DataProtection; 

namespace Vu_ACH_QR.clases
{
    internal class Cripto
    {
        private IDataProtector _protector;

        public Cripto(string serverLlave, string Protector)
        {
            _protector = ((!serverLlave.Contains(".")) ? DataProtectionProvider.Create(new DirectoryInfo("\\\\" + serverLlave)) : DataProtectionProvider.Create(new DirectoryInfo(serverLlave))).CreateProtector(Protector);
        }

        public string Desencriptar(string input)
        {
            return _protector.Unprotect(input).ToString();
        }
    }
}
