using Newtonsoft.Json;

namespace Template.API.Presentation.Configuration
{
    public class AppsettingsConfiguration
    {
        [JsonProperty("Logging")]
        public LoggingConf Logging { get; set; }

        [JsonProperty("Tracing")]
        public TracingConf Tracing { get; set; }

        [JsonProperty("Mediatr")]
        public MediatRConfig Mediatr { get; set; }

        [JsonProperty("AllowedHosts")]
        public string AllowedHosts { get; set; }
    }

    public class LoggingConf
    {
        [JsonProperty("LogLevel")]
        public LogLevelConf LogLevel { get; set; }
    }

    public class LogLevelConf
    {
        [JsonProperty("Default")]
        public string Default { get; set; }

        [JsonProperty("Microsoft")]
        public string Microsoft { get; set; }

        [JsonProperty("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class MediatRConfig
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

    public class TracingConf
    {
        [JsonProperty("Enabled")]
        public bool Enabled { get; set; }
    }
}
