namespace Naanss.Core.AI.Output
{
    public class OverallAssessment
    {
        public string Summary { get; set; } = string.Empty;

        public string OverallRiskLevel { get; set; } = "Low";

        public List<string> TopRisks { get; set; } = new();
        public List<string> TopRecommendations { get; set; } = new();
    }
}
