namespace Provider.Application.Configuration;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }     
    public Kestrel Kestrel { get; set; }
    public SerilogSettings Serilog { get; set; }
    public ProviderSettings ProviderSettings { get; set; }
}

public class ConnectionStrings
{
    public string ProviderDb { get; set; }
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

public class Kestrel
{
    public EndpointDefaults EndpointDefaults { get; set; }
}

public class EndpointDefaults
{
    public string Protocols { get; set; }
}

public class SerilogSettings
{
    public List<string> Using { get; set; }
    public MinimumLevel MinimumLevel { get; set; }
    public List<WriteTo> WriteTo { get; set; }
    public List<string> Enrich { get; set; }
    public Dictionary<string, string> Properties { get; set; }
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
    public int? FileSizeLimitBytes { get; set; }
    public string RollingInterval { get; set; }
    public bool? RollOnFileSizeLimit { get; set; }
    public string ConnectionString { get; set; }
    public string TableName { get; set; }
    public bool? AutoCreateSqlTable { get; set; }
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
}

public class ProviderSettings
{
    public MasarySettings MasarySettings { get; set; }
    public MomknSettings MomknSettings { get; set; }
}

public class MasarySettings
{
    public string MasaryPassword { get; set; }
    public string MasaryAccountNumber { get; set; }
    public string MasaryURLTransaction { get; set; }
    public string Language { get; set; }
    public string Terminal { get; set; } = string.Empty;
    public string TransactionInquiryAction { get; set; } = string.Empty;    
    public string TransactionPaymentAction { get; set; } =string.Empty;

    public int Version { get; set; }
}

public class MomknSettings
{
}
