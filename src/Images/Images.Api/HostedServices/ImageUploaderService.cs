using BuildingMarket.Common.Configuration;
using BuildingMarket.Images.Application.Features.Images.Commands.UploadPropertiesImages;
using MediatR;
using Microsoft.Extensions.Options;
using NCrontab;

namespace BuildingMarket.Images.Api.HostedServices
{
    public class ImageUploaderService(
        ILogger<ImageUploaderService> logger,
        IOptions<WorkerConfiguration> workerConfig,
        IMediator mediator)
        : BackgroundService
    {
        private readonly ILogger<ImageUploaderService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly CrontabSchedule _schedule = CrontabSchedule.Parse(workerConfig.Value.CronSchedule);
        private readonly int PeriodInSeconds = workerConfig.Value.PeriodInSeconds;

        protected DateTime nextRun = DateTime.UtcNow;
        internal bool IsForced { get; set; } = false;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("{0} is started", nameof(ImageUploaderService));

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (DateTime.UtcNow > nextRun || IsForced)
                    {
                        await _mediator.Send(new UploadPropertiesImagesCommand(), cancellationToken);
                        nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                        IsForced = false;
                    }

                    await Task.Delay(PeriodInSeconds * 1000, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while trying to update redis!");

                    // Wait 5 minutes before trying again.
                    await Task.Delay(300_000, cancellationToken);
                }
            }
        }
    }
}
