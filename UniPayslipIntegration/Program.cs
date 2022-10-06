using Softrig;
using UniPayslipIntegration;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<SoftRigSettings>(configuration.GetSection(nameof(SoftRigSettings)));
        services.Configure<SupabaseSettings>(configuration.GetSection(nameof(SupabaseSettings)));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
