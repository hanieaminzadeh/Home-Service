namespace HomeService.Core.Entities.Configurations;

public class SiteSettings
{
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }
    public Connectionstrings ConnectionStrings { get; set; }
    public Seqconfigurations SeqConfigurations { get; set; }
}

public class Logging
{
    public Loglevel LogLevel { get; set; }
}

public class Loglevel
{
    public string Default { get; set; }
    public string MicrosoftAspNetCore { get; set; }
}

public class Connectionstrings
{
    public string AppConnectionString { get; set; }
}

public class Seqconfigurations
{
    public string Url { get; set; }
    public string apiKey { get; set; }
    public string MinimumLevel { get; set; }
}
