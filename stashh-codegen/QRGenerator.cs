using System.Drawing;
using System.Drawing.Drawing2D;
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
        var generator = new PayloadGenerator.Url($"{Config.GetString(Config.Constants.UrlBase)}{code}");

        var qrCodeData = _generator.CreateQrCode(generator);
        var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeAsPng = qrCode.GetGraphic(5, drawQuietZones: false);

        await File.WriteAllBytesAsync(Path.Join(Config.GetString(Config.Constants.OutputDirectory), $"{code}.png"), qrCodeAsPng);
    }


}