using QRCoder;

namespace StorageProject.Api.Controllers
{
    public class QrCodesController
    {
        public object? GenerateQrCode(string coreValue)
        {
            if (coreValue == null) { return null; }

            else
            {
                var qrCodeGenerator = new QRCodeGenerator();
                var qrCodeData = qrCodeGenerator.CreateQrCode(coreValue, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new SvgQRCode(qrCodeData);
                return qrCode.GetGraphic(viewBox: new System.Drawing.Size(60, 60), drawQuietZones: true, sizingMode: SvgQRCode.SizingMode.ViewBoxAttribute);
            }
        }
    }
}
