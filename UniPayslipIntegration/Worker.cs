using Supabase.Service;

namespace UniPayslipIntegration
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var supaBase = new SupaBaseService();
            supaBase.SupaBaseClientConnection();
            supaBase.SupaBaseCompanyRun();
            supaBase.SupaBaseCompanyAdminRun();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);

                supaBase.SupaBaseEmployeeRun();
            }
        }
    }
}