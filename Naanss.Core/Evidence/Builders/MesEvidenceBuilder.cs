using Naanss.Core.Evidence;
using Naanss.Core.Models;
using System.Linq;

namespace Naanss.Core.Evidence.Builders
{
    public class MesEvidenceBuilder
    {
        public MesEvidence Build(MesDbProfile profile, int topN = 3)
        {
            var evidence = new MesEvidence
            {
                DatabaseName = profile.DatabaseName
            };

            var riskyTables = profile.Tables
                .Select(t => new
                {
                    Table = t,
                    RiskScore = CalculateRiskScore(t)
                })
                .OrderByDescending(x => x.RiskScore)
                .Take(topN);

            foreach (var item in riskyTables)
            {
                var table = item.Table;

                evidence.Tables.Add(new TableEvidence
                {
                    Name = table.Name,
                    RowCount = table.RowCount,
                    HasPrimaryKey = table.Columns.Any(c => c.IsPrimaryKey),
                    IndexCount = table.Indexes.Count,
                    IsTimeSeriesCandidate = IsTimeSeries(table),
                    RiskScore = item.RiskScore
                });
            }

            return evidence;
        }

        private int CalculateRiskScore(MesTable table)
        {
            int score = 0;

            if (table.RowCount > 10_000_000) score += 40;
            else if (table.RowCount > 1_000_000) score += 25;

            if (!table.Columns.Any(c => c.IsPrimaryKey)) score += 30;

            if (table.Indexes.Count > 20) score += 20;
            else if (table.Indexes.Count > 5) score += 10;

            if (IsTimeSeries(table)) score += 15;

            return score;
        }

        private bool IsTimeSeries(MesTable table)
        {
            var name = table.Name.ToLowerInvariant();

            return
                name.Contains("history") ||
                name.Contains("log") ||
                name.Contains("trace") ||
                name.Contains("audit") ||
                name.Contains("event") ||
                table.RowCount > 1_000_000;
        }
    }
}
