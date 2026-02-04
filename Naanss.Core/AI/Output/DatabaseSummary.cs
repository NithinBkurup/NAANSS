namespace Naanss.Core.AI.Output
{
    public class DatabaseSummary
    {
        public string Name { get; set; } = string.Empty;

        public string OverallRiskLevel { get; set; } = "Low"; 
        // Low / Medium / High

        public List<string> KeyFindings { get; set; } = new();

        public List<string> PriorityActions { get; set; } = new();
    }
}
