using Quartz;
using Vu_ACH_QR;
using Vu_ACH_QR.clases;
using Vu_ACH_QR.core;
Config_quartz configq = null;
IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {         options.ServiceName = "Vu_QR_ACH_Entrantes";
    }).ConfigureServices((hostContext, services) =>
    {
        IConfiguration _configuration = hostContext.Configuration;
        Config_quartz configquartz= _configuration.GetSection("VU").Get<Config_quartz>();
        Encript valueconfig = new Encript(configquartz);
        configq = valueconfig.getParameters();
        services.AddSingleton(configq);
        services.AddQuartz(q =>
        { 
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = configq.Hilos; }); 
        }); 
        services.AddQuartzHostedService( q => q.WaitForJobsToComplete = true); 
    }).Build();
ISchedulerFactory schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
IScheduler scheduler = await schedulerFactory.GetScheduler();
ControllerMain controllerJobs = new ControllerMain(scheduler, configq);  
await host.RunAsync();
