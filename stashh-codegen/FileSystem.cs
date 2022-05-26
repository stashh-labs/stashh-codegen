using System.Globalization;

namespace stashh_codegen;

using CsvHelper;

public static class FileSystem
{
    private const string _outputDirectory = "codes";

    public static bool OutputDirectoryExists()
    {
        return Directory.Exists(_outputDirectory);
    }
    
    public static string EnsureOutputDirectoryExists()
    {
        Directory.CreateDirectory(_outputDirectory);

        return _outputDirectory;
    }
    
    public static async Task WriteCodesToFile(IEnumerable<ClaimCode> codes)
    {
        Directory.CreateDirectory(_outputDirectory);

        await using var writer = new StreamWriter(Path.Join(_outputDirectory, "codes.csv"));
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

    public static async Task WriteQrToFile()
    {
    }
}