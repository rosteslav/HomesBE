using BuildingMarket.Common.Configuration;
using BuildingMarket.Properties.Application.Features.Properties.Commands.UploadRecommendations;
using MediatR;
using Microsoft.Extensions.Options;
using NCrontab;

namespace BuildingMarket.Properties.Api.HostedServices
{
    public class RecommendationUploaderService(
        ILogger<RecommendationUploaderService> logger,
        IOptions<WorkerConfiguration> workerConfig,
        IMediator mediator)
        : BackgroundService
    {
        private readonly ILogger<RecommendationUploaderService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly CrontabSchedule _schedule = CrontabSchedule.Parse(workerConfig.Value.CronSchedule);
        private readonly int PeriodInSeconds = workerConfig.Value.PeriodInSeconds;

        protected DateTime nextRun = DateTime.UtcNow;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("{0} is started", nameof(RecommendationUploaderService));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (DateTime.UtcNow > nextRun)
                    {
                        await _mediator.Send(new UploadRecommendationsCommand(), cancellationToken);
                        nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
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
