namespace Naanss.Core.Evidence
{
    public class ServerEvidence
    {
        public string HostName { get; set; } = string.Empty;
        public string SqlVersion { get; set; } = string.Empty;
        public int CpuCores { get; set; }
        public int MemoryGB { get; set; }

        public string DiskType { get; set; } = "SSD"; // HDD / SSD / SAN
        public bool IsVirtualized { get; set; }

        public bool IsMemoryPressureDetected { get; set; }
        public bool IsCpuPressureDetected { get; set; }
    }
}
