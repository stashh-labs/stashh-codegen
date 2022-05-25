using System.Collections;
using System.Globalization;
using System.Text;
using CsvHelper;
using stashh_codegen;

const string Digits = "BCDFGHJKLMNPQRSTVWXYZ";
var _random = new Random();

var codes = GenerateCodes();

using var writer = new StreamWriter("codes.csv");
using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

csv.WriteRecords(codes);

string GenerateSeriesCodeForIndex(int index)
{
    var seriesIndex = index / 100;

    var seriesA = Digits[(seriesIndex / Digits.Length) % Digits.Length];
    var seriesB = Digits[seriesIndex % Digits.Length];

    return $"{seriesA}{seriesB}";
}

IEnumerable<ClaimCode> GenerateCodes()
{
    var codes = new Dictionary<string, int>();

    do
    {
        var newCode = $"{GenerateSeriesCodeForIndex(codes.Count)}{GenerateCode(8)}";

        if (!codes.ContainsKey(newCode))
        {
            codes.Add(newCode, 0);
        }
    } while (codes.Count < 5000);

    return codes.Select(c => new ClaimCode { Code = c.Key });
}

string GenerateCode(int size)
{
    var builder = new StringBuilder(size);

    for (var i = 0; i < size; i++)
    {
        builder.Append(Digits[_random.Next(0, Digits.Length)]);
    }

    return builder.ToString();
}