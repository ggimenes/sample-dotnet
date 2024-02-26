using Microsoft.AspNetCore.Mvc;
using SampleDotnet.DDD.Abstractions;

namespace SampleDotnet.AspNet
{
    public static class NotificationExtensions
    {
        public static ActionResult HandleErrors(this INotification notification)
        {
            var errors = new ErrorDto()
            {
                Errors = notification.GetValidationErrors()
            };
            return new BadRequestObjectResult(errors);
        }
    }
}
