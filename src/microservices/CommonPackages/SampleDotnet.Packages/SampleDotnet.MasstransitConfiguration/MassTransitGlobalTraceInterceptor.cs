using MassTransit;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SampleDotnet.MassTransit.ActivityTracing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SampleDotnet.MasstransitConfiguration
{
    public class MassTransitGlobalTraceInterceptor : IGlobalTraceInterceptor
    {
        public Task Intercept(object context, Activity traceActivity)
        {
            string body = GetBody(context, out bool masstransitEnvelope);
            string pathPrefix = masstransitEnvelope ? "message." : "";

            if (body == null)
                return Task.CompletedTask;

            JObject jObj = ParseJson(body);

            if (jObj == null)
                return Task.CompletedTask;

            string correlationId = jObj.SelectToken(pathPrefix + "correlationId")?.Value<string>();

            var jObjMessage = masstransitEnvelope ? jObj.SelectToken("message") : jObj;
            var dicJToken = ((IDictionary<string, JToken>)jObjMessage);
            var dicBody = dicJToken.ToDictionary(x => x.Key, x => x.Value.Value<object>());

            traceActivity
                .AddBaggage("correlation-id", correlationId)
                .AddTag("correlation-id", correlationId)
                .AddEvent(new ActivityEvent("body", tags: new ActivityTagsCollection(dicBody)));

            return Task.CompletedTask;
        }

        private string GetBody(object context, out bool masstransitEnvelope)
        {
            string body = null;

            // tried to avoid reflection but I didn't figure out how do this with MassTransit. Didn't found any public property or method
            if (context is ConsumeContext)
            {
                masstransitEnvelope = false;
                var message = context.GetType().GetProperty("Message", BindingFlags.Public | BindingFlags.Instance).GetValue(context);
                if (message != null)
                {
                    var settings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.None,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        FloatFormatHandling = FloatFormatHandling.DefaultValue,
                        FloatParseHandling = FloatParseHandling.Decimal,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        Converters = new[] { new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal } }
                    };

                    body = JsonConvert.SerializeObject(message, settings);
                }
            }
            else
            {
                masstransitEnvelope = true;
                body = context.GetType().GetProperty("BodyText", BindingFlags.Public | BindingFlags.Instance).GetValue(context)?.ToString();
            }
            return body;
        }

        private JObject ParseJson(string str)
        {
            if (!IsValidJson(str))
                return null;

            try
            {
                return JObject.Parse(str);
            }
            catch { return null; }
        }

        private bool IsValidJson(string str)
        {
            return (str.StartsWith("{") && str.EndsWith("}"));
        }
    }
}
