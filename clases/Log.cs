using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vu_ACH_QR.clases
{
    internal class Log
    {
        public string NameLog = "Log";
        public string FullPath = "";
        public Log(string fpath, string namelog) {
            this.NameLog = namelog;
            this.FullPath = fpath;
        }
        public async void GrabaLogDia(string mensaje)
        {
            string path = FullPath + this.NameLog + "-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter file = new StreamWriter(path, true);
            await file.WriteLineAsync(DateTime.Now.ToString("HHmmss") + ":" + mensaje);
            file.Flush();
            file.Close();
            file.Dispose();
        }
        public async void GrabaLogHora(string mensaje)
        {
                string path = FullPath + this.NameLog + "-" + DateTime.Now.ToString("yyyyMMdd-HH") + ".txt";
                StreamWriter file = new StreamWriter(path, true);
                await file.WriteLineAsync(DateTime.Now.ToString("HHmmss")+":" + mensaje); 
                file.Flush();
                file.Close();
                file.Dispose(); 
        }
        public async void GrabaLogMinuto(string mensaje)
        { 
            string path = FullPath + this.NameLog + "-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            StreamWriter file = new StreamWriter(path, true);
            await file.WriteLineAsync(DateTime.Now.ToString("HHmmss") + ":" + mensaje);
            file.Flush();
            file.Close();
            file.Dispose(); 
        }
         
        public string getKey(int length)
        {
            Random random = new Random();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(characters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
