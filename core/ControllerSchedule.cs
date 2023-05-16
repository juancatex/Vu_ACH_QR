using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vu_ACH_QR.clases;

namespace Vu_ACH_QR.core
{
    internal abstract class ControllerSchedule
    { 
        protected IScheduler scheduler;
        protected Config_quartz config;
        public ControllerSchedule(IScheduler sched, Config_quartz conf) {
            this.scheduler = sched;
            this.config = conf;
            ServiceJobs();
        }
         
        protected abstract void ServiceJobs();
    }
}
