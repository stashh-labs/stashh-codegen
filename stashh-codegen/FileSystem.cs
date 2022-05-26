using System.Globalization;

namespace stashh_codegen;

using CsvHelper;

public static class FileSystem
{
    public static bool OutputDirectoryExists()
    {
        return Directory.Exists(Config.GetString(Config.Constants.OutputDirectory));
    }

    public static string EnsureOutputDirectoryExists()
    {
        Directory.CreateDirectory(Config.GetString(Config.Constants.OutputDirectory));

        return Config.GetString(Config.Constants.OutputDirectory);
    }

    public static async Task WriteCodesToFile(IEnumerable<ClaimCode> codes)
    {
        Directory.CreateDirectory(Config.GetString(Config.Constants.OutputDirectory));

        await using var writer = new StreamWriter(Path.Join(Config.GetString(Config.Constants.OutputDirectory), $"{Config.GetString(Config.Constants.OutputFilename)}.csv"));
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        try
        {
            await csv.WriteRecordsAsync(codes);
        }
        finally
        {
            await writer.FlushAsync();
        }
    }
}