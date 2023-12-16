using BuildingMarket.Common.Configuration;
using BuildingMarket.Images.Application.Features.Test.Commands.UpdateTest;
using MediatR;
using Microsoft.Extensions.Options;
using NCrontab;

namespace BuildingMarket.Images.Api.HostedServices
{
    public class TestService(
        ILogger<TestService> logger,
        IOptions<WorkerConfiguration> workerConfig,
        IMediator mediator) 
        : BackgroundService
    {
        private readonly ILogger<TestService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly CrontabSchedule _schedule = CrontabSchedule.Parse(workerConfig.Value.CronSchedule);

        protected DateTime nextRun = DateTime.UtcNow;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
            _logger.LogInformation("{0} is started", nameof(TestService));

            while (!cancellationToken.IsCancellationRequested) 
            {
                try
                {
                    if (DateTime.UtcNow > nextRun)
                    {
                        await _mediator.Send(new UpdateTestCommand(), cancellationToken);

                        nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
                    }

                    await Task.Delay(Math.Max(Convert.ToInt32((nextRun - DateTime.UtcNow).TotalMilliseconds), 1000), cancellationToken); //wait at least one second
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
