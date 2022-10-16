using System.Threading.Tasks;

namespace Adaca.Api.Infrastructure.KeepAlive.Publisher
{
    public interface IKeepAliveEventPublisher
    {
        Task PublishKeepAliveEvent();
    }
}
