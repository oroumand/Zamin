namespace MiniBlog.Endpoints.WorkerService.Workers;

public class WorkerBackgroundService : BackgroundService
{
    private readonly ILogger<WorkerBackgroundService> _logger;

    public WorkerBackgroundService(ILogger<WorkerBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("BackgroundService running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
