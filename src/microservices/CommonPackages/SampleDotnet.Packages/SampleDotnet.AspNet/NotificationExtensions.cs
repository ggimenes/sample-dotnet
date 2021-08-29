using Microsoft.AspNetCore.Mvc;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
