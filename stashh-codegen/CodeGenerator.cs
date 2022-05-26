using System.Text;

namespace stashh_codegen;

public static class CodeGenerator
{
    private static readonly Random _random = new Random();
    private const string Digits = "BCDFGHJKLMNPQRSTVWXYZ";

    public static IEnumerable<ClaimCode> Generate()
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

    private static string GenerateSeriesCodeForIndex(int index)
    {
        var seriesIndex = index / 100;

        var seriesA = Digits[(seriesIndex / Digits.Length) % Digits.Length];
        var seriesB = Digits[seriesIndex % Digits.Length];

        return $"{seriesA}{seriesB}";
    }

    private static string GenerateCode(int size)
    {
        var builder = new StringBuilder(size);

        for (var i = 0; i < size; i++)
        {
            builder.Append(Digits[_random.Next(0, Digits.Length)]);
        }

        return builder.ToString();
    }
}