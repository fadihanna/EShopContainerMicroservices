namespace MagicServices.API.Configurations
{
    using System.Collections.Generic;

    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public SwaggerConfig SwaggerConfig { get; set; }
        public MessageBroker MessageBroker { get; set; }
        public FeatureManagement FeatureManagement { get; set; }
        public Logging Logging { get; set; }
        public SerilogConfig Serilog { get; set; }
        public LocalizationConfig Localization { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class ConnectionStrings
    {
        public string Database { get; set; }
    }

    public class SwaggerConfig
    {
        public List<string> AssembliesNames { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class MessageBroker
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class FeatureManagement
    {
        public bool OrderFullfilment { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class SerilogConfig
    {
        public List<string> Using { get; set; }
        public MinimumLevel MinimumLevel { get; set; }
        public List<WriteTo> WriteTo { get; set; }
        public List<string> Enrich { get; set; }
        public Properties Properties { get; set; }
    }

    public class MinimumLevel
    {
        public string Default { get; set; }
        public Dictionary<string, string> Override { get; set; }
    }

    public class WriteTo
    {
        public string Name { get; set; }
        public WriteToArgs Args { get; set; }
    }

    public class WriteToArgs
    {
        public string Path { get; set; }
        public string FileSizeLimitBytes { get; set; }
        public string RollingInterval { get; set; }
        public string RollOnFileSizeLimit { get; set; }
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public bool AutoCreateSqlTable { get; set; }
        public string RestrictedToMinimumLevel { get; set; }
        public string Filter { get; set; }
        public ColumnOptionsSection ColumnOptionsSection { get; set; }
    }

    public class ColumnOptionsSection
    {
        public List<AdditionalColumn> AdditionalColumns { get; set; }
        public Properties Properties { get; set; }
    }

    public class AdditionalColumn
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
    }

    public class Properties
    {
        public bool ConvertToJson { get; set; }
        public string Environment { get; set; }
    }

    public class LocalizationConfig
    {
        public string DefaultCulture { get; set; }
    }
}
