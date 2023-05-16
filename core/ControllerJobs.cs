using Quartz; 
using Vu_ACH_QR.clases;

namespace Vu_ACH_QR.core
{
    internal abstract class ControllerJobs<JOB1> : ControllerSchedule where JOB1 : IJob
    {
        public ControllerJobs(IScheduler host, Config_quartz conf) : base(host, conf) {}
        public abstract string SetCronJob1();
        public abstract string SetNameJob1();
        protected override void ServiceJobs()
        { 
            SetJob<JOB1>("1", SetNameJob1(), SetCronJob1());
        }
        public void SetJob<J>(string grupo, string name, string cronExpression) where J : IJob
        { 
            string groupName = $"Group{grupo}";
            string triggerName = $"Trigger{grupo}";  
            IJobDetail job = JobBuilder.Create<J>().WithIdentity(name, groupName).Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity(triggerName, groupName).StartNow().WithCronSchedule(cronExpression).Build();
            scheduler.ScheduleJob(job, trigger); 
        }
    }
    internal abstract class ControllerJobs<JOB1, JOB2> : ControllerJobs<JOB1> where JOB1 : IJob where JOB2 : IJob
    {
        public ControllerJobs(IScheduler host, Config_quartz conf) : base(host, conf) => SetJob<JOB2>("2", SetNameJob2(), SetCronJob2());
        public abstract string SetCronJob2();
        public abstract string SetNameJob2();
    }
    internal abstract class ControllerJobs<JOB1, JOB2, JOB3> : ControllerJobs<JOB1, JOB2> where JOB1 : IJob where JOB2 : IJob where JOB3 : IJob
    {
        public ControllerJobs(IScheduler host, Config_quartz conf) : base(host, conf) => SetJob<JOB3>("3", SetNameJob3(), SetCronJob3());
        public abstract string SetCronJob3();
        public abstract string SetNameJob3();
    }
    internal abstract class ControllerJobs<JOB1, JOB2, JOB3, JOB4> : ControllerJobs<JOB1, JOB2, JOB3> where JOB1 : IJob where JOB2 : IJob where JOB3 : IJob where JOB4 : IJob
    {
        public ControllerJobs(IScheduler host, Config_quartz conf) : base(host, conf) => SetJob<JOB3>("4", SetNameJob4(), SetCronJob4());
        public abstract string SetCronJob4();
        public abstract string SetNameJob4();
    }
    internal abstract class ControllerJobs<JOB1, JOB2, JOB3, JOB4, JOB5> : ControllerJobs<JOB1, JOB2, JOB3, JOB4>  where JOB1 : IJob where JOB2 : IJob where JOB3 : IJob where JOB4 : IJob where JOB5 : IJob
    {
        public ControllerJobs(IScheduler host, Config_quartz conf) : base(host, conf) => SetJob<JOB3>("5", SetNameJob5(), SetCronJob5());
        public abstract string SetCronJob5();
        public abstract string SetNameJob5();
    }
}
