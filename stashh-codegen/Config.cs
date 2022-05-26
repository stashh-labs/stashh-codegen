using Microsoft.Extensions.Configuration;

namespace stashh_codegen;

public static class Config
{
    private static IConfigurationRoot _config = null;
    private const string _configFilename = "appsettings.json";

    public static string GetString(string key)
    {
        EnsureConfigLoaded();

        var section = _config.GetSection(key);

        return section is not null ? section.Value : null;
    }

    public static int GetInt(string key)
    {
        return int.Parse(GetString(key));
    }

    private static void EnsureConfigLoaded()
    {
        _config ??= new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_configFilename)
            .Build();
    }

    public static class Constants
    {
        public const string OutputDirectory = "output:directory";
        public const string OutputFilename = "output:filename";
        public const string UrlBase = "output:urlBase";
        public const string SeriesSize = "output:seriesSize";
        public const string Digits = "output:digits";
        public const string TotalCodes = "output:totalCodes";
        public const string CodeLength = "output:codeLength";
    }
}