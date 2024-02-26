using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Abstractions
{
    public interface IEventDispatcher
    {
        Task Dispatch(IEnumerable<IDomainEvent> domainEvents);
    }
}
