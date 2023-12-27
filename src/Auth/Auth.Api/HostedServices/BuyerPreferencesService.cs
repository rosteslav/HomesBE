using BuildingMarket.Auth.Application.Features.Preferences;
using BuildingMarket.Common.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using NCrontab;

namespace BuildingMarket.Auth.Api.HostedServices
{
    public class BuyerPreferencesService(
        ILogger<BuyerPreferencesService> logger,
        IOptions<WorkerConfiguration> workerConfig,
        IMediator mediator)
        : BackgroundService
    {
        private readonly ILogger<BuyerPreferencesService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly CrontabSchedule _schedule = CrontabSchedule.Parse(workerConfig.Value.CronSchedule);
        private readonly int PeriodInSeconds = workerConfig.Value.PeriodInSeconds;

        protected DateTime nextRun = DateTime.UtcNow;

        internal bool IsForced { get; set; } = false;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("{serviceName} is started.", nameof(BuyerPreferencesService));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (DateTime.UtcNow > nextRun || IsForced)
                    {
                        await _mediator.Send(new SetBuyersPreferencesCommand(), cancellationToken);

                        nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                        IsForced = false;
                    }

                    await Task.Delay(PeriodInSeconds * 1000, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while trying to update redis!");

                    await Task.Delay(300_000, cancellationToken);
                }
            }
        }
    }
}
