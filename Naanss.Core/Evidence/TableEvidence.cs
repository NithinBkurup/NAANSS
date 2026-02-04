namespace Naanss.Core.Evidence
{
    public class TableEvidence
    {
        public string Name { get; set; } = string.Empty;
        public long RowCount { get; set; }

        public bool HasPrimaryKey { get; set; }
        public int IndexCount { get; set; }

        public bool IsTimeSeriesCandidate { get; set; }
        public bool IsMasterDataCandidate { get; set; }
        public bool IsTransactionDataCandidate { get; set; }

        public long EstimatedDailyGrowth { get; set; }
        public int StatisticsAgeDays { get; set; }

        public bool HasForeignKeys { get; set; }
        public bool HasTriggers { get; set; }
        public int RiskScore { get; set; }
    }
}
