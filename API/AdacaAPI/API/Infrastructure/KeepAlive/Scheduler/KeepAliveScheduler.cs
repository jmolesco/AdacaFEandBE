using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Adaca.Api.Infrastructure.KeepAlive.Publisher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Adaca.Api.Infrastructure.KeepAlive.Scheduler
{
    public class KeepAliveScheduler : IHostedService, IDisposable
    {
        private Timer _timer;
        private const string DefaultInterval = "5";
        private readonly ILogger<KeepAliveScheduler> _logger;
        private readonly IKeepAliveEventPublisher _eventPublisher;
        private readonly IConfiguration _configuration;

        public KeepAliveScheduler(IKeepAliveEventPublisher eventPublisher, ILogger<KeepAliveScheduler> logger, IConfiguration configuration)
        {
            _eventPublisher = eventPublisher;
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = GetIntervalOrDefault();
            _logger.LogInformation($"KeepAlive Scheduler is running. Will send KeepAlive every {interval} minutes");

            _timer = new Timer(PublishKeepAlive, null, TimeSpan.Zero,
              TimeSpan.FromMinutes(interval));

            return Task.CompletedTask;
        }

        private int GetIntervalOrDefault()
        {
            var intervalString = _configuration["KeepAlive:Interval"];
            if (string.IsNullOrWhiteSpace(intervalString))
            {
                intervalString = DefaultInterval;
            }
            return Convert.ToInt32(intervalString);
        }

        private void PublishKeepAlive(object state)
        {
            _eventPublisher.PublishKeepAliveEvent();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("KeepAlive Scheduler is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
