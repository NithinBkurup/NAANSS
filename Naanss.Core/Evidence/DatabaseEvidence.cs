namespace Naanss.Core.Evidence
{
    public class DatabaseEvidence
    {
        public string Name { get; set; } = string.Empty;
        public long SizeMB { get; set; }
        public int CompatibilityLevel { get; set; }

        public bool IsAutoShrinkEnabled { get; set; }
        public bool IsAutoCloseEnabled { get; set; }

        public int FileCount { get; set; }
        public bool HasRecentBackups { get; set; }
    }
}
