
using Finaps.EventBus.Core.Abstractions;
using Microsoft.Extensions.Logging;
using Adaca.Api.Infrastructure.KeepAlive.Event;
using System;
using System.Threading.Tasks;

namespace Adaca.Api.Infrastructure.KeepAlive.Publisher
{
    public class KeepAliveEventPublisher : IKeepAliveEventPublisher
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<KeepAliveEventPublisher> _logger;
        public KeepAliveEventPublisher(
          IEventBus eventBus, ILogger<KeepAliveEventPublisher> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public Task PublishKeepAliveEvent()
        {
            var keepAlive = new RegisKeepAliveEvent()
            {
                CreationDate = DateTime.Now
            };
            _logger.LogDebug($"Sending KeepAlive message with Id {keepAlive.Id}");
            _eventBus.Publish(keepAlive);
            return Task.CompletedTask;
        }
    }
}
