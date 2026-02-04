namespace Naanss.Core.AI.Output
{
    public class AnalysisMeta
    {
        public string PromptVersion { get; set; } = string.Empty;
        public string ModelUsed { get; set; } = string.Empty;

        public DateTime AnalyzedAtUtc { get; set; } = DateTime.UtcNow;

        // 0.0 – 1.0
        public double OverallConfidence { get; set; }

        public bool IsChatAllowed => OverallConfidence >= 0.90;
    }
}
