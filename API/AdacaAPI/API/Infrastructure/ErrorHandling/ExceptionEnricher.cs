using Serilog.Core;
using Serilog.Events;
using System.Text.RegularExpressions;

namespace Adaca.Api.Infrastructure.ErrorHandling
{
    internal class ExceptionEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception == null)
                return;

            var stacktrace = Regex.Replace(logEvent.Exception.StackTrace.Trim(), @"\n\s*", ", ");
            var exception = $"{logEvent.Exception.GetType()}: {logEvent.Exception.Message} [{stacktrace}]";
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("SingleLineException", exception));
        }
    }
}
