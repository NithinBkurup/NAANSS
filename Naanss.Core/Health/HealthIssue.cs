namespace Naanss.Core.Health
{
    public enum IssueSeverity
    {
        Info,
        Warning,
        Critical
    }

    public class HealthIssue
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IssueSeverity Severity { get; set; }

        public string Recommendation { get; set; } = string.Empty;
        public int ImpactScore { get; set; }
    }
}
