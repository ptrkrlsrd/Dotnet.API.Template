using Newtonsoft.Json;

namespace Template.API.Presentation.Configuration
{
    public class AppsettingsConfiguration
    {
        [JsonProperty("Logging")]
        public LoggingConfiguration Logging { get; set; }

        [JsonProperty("Tracing")]
        public TracingConfiguration Tracing { get; set; }

        [JsonProperty("Mediatr")]
        public MediatRConfiguration Mediatr { get; set; }

        [JsonProperty("AllowedHosts")]
        public string AllowedHosts { get; set; }
    }

    public class LoggingConfiguration
    {
        [JsonProperty("LogLevel")]
        public LogLevelConfiguration LogLevel { get; set; }
    }

    public class LogLevelConfiguration
    {
        [JsonProperty("Default")]
        public string Default { get; set; }

        [JsonProperty("Microsoft")]
        public string Microsoft { get; set; }

        [JsonProperty("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class MediatRConfiguration
    {
        [JsonProperty("Middleware")]
        public MediatRMiddlewareConf Middleware { get; set; }
    }

    public class MediatRMiddlewareConf
    {
        [JsonProperty("Tracing")]
        public bool Tracing { get; set; }

        [JsonProperty("Logging")]
        public bool Logging { get; set; }
    }

    public class TracingConfiguration
    {
        [JsonProperty("Enabled")]
        public bool Enabled { get; set; }
    }
}
