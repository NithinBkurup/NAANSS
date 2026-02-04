namespace Naanss.Core.AI.Output
{
    public class Recommendation
    {
        public string Action { get; set; } = string.Empty;

        public string Rationale { get; set; } = string.Empty;

        public string RiskIfIgnored { get; set; } = string.Empty;

        public string SafetyLevel { get; set; } = "Conservative";
        // Conservative / Moderate / Aggressive
    }
}
