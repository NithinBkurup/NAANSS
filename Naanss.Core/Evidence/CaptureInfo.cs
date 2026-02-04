namespace Naanss.Core.Evidence
{
    public class CaptureInfo
    {
        public DateTime CapturedAtUtc { get; set; } = DateTime.UtcNow;
        public string CapturedBy { get; set; } = "Naanss";
        public string NaanssVersion { get; set; } = "0.1-alpha";
        public string Environment { get; set; } = "Dev"; // Dev / Test / Prod
    }
}
