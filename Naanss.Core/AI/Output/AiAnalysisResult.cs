namespace Naanss.Core.AI.Output
{
    public class AiAnalysisResult
    {
        public AnalysisMeta Meta { get; set; } = new();
        public DatabaseSummary Database { get; set; } = new();

        public List<TableAnalysis> Tables { get; set; } = new();

        public OverallAssessment Overall { get; set; } = new();
    }
}
