namespace Naanss.Core.Health
{
    public class MesHealthReport
    {
        public string DatabaseName { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public List<HealthIssue> Issues { get; set; } = new();

        public int HealthScore => Math.Max(0, 100 - Issues.Sum(i => i.ImpactScore));
    }
}
