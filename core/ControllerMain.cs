using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vu_ACH_QR.clases;

namespace Vu_ACH_QR.core
{
    internal class ControllerMain : ControllerJobs<service_Reg_Vu>
    {
        public ControllerMain(IScheduler host, Config_quartz conf) : base(host, conf) {}
        public override string SetCronJob1()
        {
           return config.CronJob;
        } 
        public override string SetNameJob1()
        {
            return "principal job";
        }

    }
}
