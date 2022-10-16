using Finaps.EventBus.Core.Abstractions;
using Microsoft.Extensions.Logging;
using Adaca.Api.Infrastructure.KeepAlive.Event;
using System;
using System.Threading.Tasks;

namespace Adaca.Api.Infrastructure.KeepAlive.Handler
{
    public class KeepAliveEventHandler : IIntegrationEventHandler<RegisKeepAliveEvent>
    {
        private readonly ILogger _logger;
        public KeepAliveEventHandler(
          ILogger<KeepAliveEventHandler> logger
        )
        {
            _logger = logger;
        }

        public Task Handle(RegisKeepAliveEvent @event)
        {
            var latency = (DateTime.Now - @event.CreationDate).TotalMilliseconds;
            _logger.LogDebug($"Received KeepAlive with Id {@event.Id}. Latency: {latency}");
            return Task.CompletedTask;
        }
    }
}
