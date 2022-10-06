using Microsoft.Extensions.Options;
using Softrig;
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

            var uniDataService = new UniDataService();
            uniDataService.InitSoftRigApi(_softRigSettings.softrigUrl, _softRigSettings.authUrl, _softRigSettings.certificatePassword, _softRigSettings.clientID);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(10000, stoppingToken);
                await employeeRun.GetSupaBaseEmployee();
            }
        }
    }
}