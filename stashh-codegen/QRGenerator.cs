using System.Drawing;
using System.Text;
using QRCoder;

namespace stashh_codegen;

public static class QRGenerator
{
    private static readonly QRCodeGenerator _generator = new QRCodeGenerator();
    
    public static async Task Generate(IEnumerable<ClaimCode> codes)
    {
        foreach (var code in codes)
        {
            await GenerateQrCode(code.Code);
        }
    }
    
    private static async Task GenerateQrCode(string code)
    {
        var generator = new PayloadGenerator.Url($"https://claim.stashh.io/?code={code}");

        var qrCodeData = _generator.CreateQrCode(generator);
        var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeAsPng = qrCode.GetGraphic(5, drawQuietZones: false);

        var OutputDirectory = FileSystem.EnsureOutputDirectoryExists();

        await File.WriteAllBytesAsync(Path.Join(OutputDirectory, $"{code}.png"), qrCodeAsPng);
    }


}