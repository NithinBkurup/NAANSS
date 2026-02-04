namespace Naanss.Core.AI.Output
{
    public class TableAnalysis
    {
        public string TableName { get; set; } = string.Empty;

        public string RiskLevel { get; set; } = "Low"; 
        // Low / Medium / High

        // 0.0 – 1.0
        public double Confidence { get; set; }

        public List<Finding> Findings { get; set; } = new();
        public List<Recommendation> Recommendations { get; set; } = new();

        public List<string> DoNotChange { get; set; } = new();
    }
}
