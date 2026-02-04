namespace Naanss.Core.Evidence
{
    public class MesEvidence
    {
        public CaptureInfo Capture { get; set; } = new();
        public ServerEvidence Server { get; set; } = new();
        public DatabaseEvidence Database { get; set; } = new();
        public string DatabaseName { get; set; } = string.Empty;
        public DateTime CapturedAtUtc { get; set; } = DateTime.UtcNow;
        public List<TableEvidence> Tables { get; set; } = new();
    }
}
