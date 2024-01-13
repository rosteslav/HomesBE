using BuildingMarket.Common.Configuration;
using BuildingMarket.Properties.Application.Features.Properties.Commands.UploadProperties;
using MediatR;
using Microsoft.Extensions.Options;
using NCrontab;

namespace BuildingMarket.Properties.Api.HostedServices
{
    public class PropertiesUploaderService(
        IMediator mediator, 
        ILogger<PropertiesUploaderService> logger,
        IOptions<WorkerConfiguration> workerConfig) 
        : BackgroundService
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<PropertiesUploaderService> _logger = logger;
        private readonly CrontabSchedule _schedule = CrontabSchedule.Parse(workerConfig.Value.CronSchedule);
        private readonly int PeriodInSeconds = workerConfig.Value.PeriodInSeconds;

        protected DateTime nextRun = DateTime.UtcNow;
        internal bool IsForced { get; set; } = false;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("{service} is started", nameof(PropertiesUploaderService));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (DateTime.UtcNow > nextRun || IsForced)
                    {
                        await _mediator.Send(new UploadPropertiesCommand(), cancellationToken);
                        nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                        IsForced = false;
                    }

                    await Task.Delay(PeriodInSeconds * 1000, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while trying to update Redis!");

                    // Wait 5 minutes before trying again.
                    await Task.Delay(300_000, cancellationToken);
                }
            }
        }
    }
}
