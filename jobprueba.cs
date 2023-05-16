using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vu_ACH_QR.clases;

namespace Vu_ACH_QR
{
    internal class jobprueba : IJob
    {
        private readonly Config_quartz _dataconfig;
        private readonly ILogger<service_Reg_Vu> _logger;
        public jobprueba(ILogger<service_Reg_Vu> logger, Config_quartz options)
        {
            _logger = logger;
            _dataconfig = options;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("***************** prueba en   {0} -----------   {1}<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<---",  DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), _dataconfig.CronJob);
            return Task.CompletedTask;
        }
    }
}
