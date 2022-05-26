using System.Text;

namespace stashh_codegen;

public static class CodeGenerator
{
    private static readonly Random _random = new Random();

    public static IEnumerable<ClaimCode> Generate()
    {
        var codes = new Dictionary<string, int>();

        do
        {
            var newCode = $"{GenerateSeriesCodeForIndex(codes.Count)}{GenerateCode(Config.GetInt(Config.Constants.CodeLength))}";

            if (!codes.ContainsKey(newCode))
            {
                codes.Add(newCode, 0);
            }
        } while (codes.Count < Config.GetInt(Config.Constants.TotalCodes));

        return codes.Select(c => new ClaimCode { Code = c.Key });
    }

    private static string GenerateSeriesCodeForIndex(int index)
    {
        var digits = Config.GetString(Config.Constants.Digits);

        var seriesIndex = index / Config.GetInt(Config.Constants.SeriesSize);

        var seriesA = digits[(seriesIndex / digits.Length) % digits.Length];
        var seriesB = digits[seriesIndex % digits.Length];

        return $"{seriesA}{seriesB}";
    }

    private static string GenerateCode(int size)
    {
        var digits = Config.GetString(Config.Constants.Digits);
        var builder = new StringBuilder(size);

        for (var i = 0; i < size; i++)
        {
            builder.Append(digits[_random.Next(0, digits.Length)]);
        }

        return builder.ToString();
    }
}