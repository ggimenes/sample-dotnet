using System;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Abstractions
{
    public interface INotificationHandler
    {
        INotification Notification { get; set; }

        /// <summary>
        /// Dispatch events and flush log records
        /// </summary>
        Task DispatchAndFlush(Guid correlationId);
    }
}
