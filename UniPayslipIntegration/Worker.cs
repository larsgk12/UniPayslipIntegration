using Microsoft.Extensions.Options;
using Supabase.Service;
using SupabaseConnection.Service;

namespace UniPayslipIntegration
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SoftRigSettings _softRigSettings;
        private readonly SupabaseSettings _supabaseSettings;

        public Worker(ILogger<Worker> logger, IOptions<SoftRigSettings> softRigSettings, IOptions<SupabaseSettings> supabaseSettings)
        {
            _logger = logger;
            _softRigSettings = softRigSettings.Value;
            _supabaseSettings = supabaseSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var supaBase = new SupaBaseService();
            supaBase.SupaBaseClientConnection(_supabaseSettings.key, _supabaseSettings.url);
            
            var employeeRun = new EmployeeCtrl();
            //employeeRun.PostSupaBaseEmployee();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Key {key}", _supabaseSettings.url);
                _logger.LogInformation("Url {key}", _softRigSettings.authUrl);

                await Task.Delay(10000, stoppingToken);
               // await Task.Delay(1000, stoppingToken);
                
                employeeRun.GetSupaBaseEmployee();
            }
        }
    }
}