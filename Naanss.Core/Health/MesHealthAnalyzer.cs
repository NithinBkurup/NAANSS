using Naanss.Core.Contracts;
using Naanss.Core.Health;
using Naanss.Core.Models;

namespace Naanss.Core.Health.Analyzers
{
    public class MesHealthAnalyzer : IMesAnalyzer
    {
        private const long LargeTableRowThreshold = 1_000_000;

        public MesHealthReport Analyze(MesDbProfile profile)
        {
            var report = new MesHealthReport
            {
                DatabaseName = profile.DatabaseName
            };

            CheckMissingPrimaryKeys(profile, report);
            CheckMissingIndexes(profile, report);
            CheckLargeTables(profile, report);

            return report;
        }

        // ---------------- RULE 1 ----------------
        private void CheckMissingPrimaryKeys(MesDbProfile profile, MesHealthReport report)
        {
            foreach (var table in profile.Tables)
            {
                var hasPrimaryKey = table.Columns.Any(c => c.IsPrimaryKey);

                if (!hasPrimaryKey)
                {
                    report.Issues.Add(new HealthIssue
                    {
                        Code = "NO_PRIMARY_KEY",
                        Severity = IssueSeverity.Critical,
                        ImpactScore = 20,
                        Description = $"Table '{table.Name}' has no primary key.",
                        Recommendation =
                            "Define a primary key to ensure data integrity, " +
                            "enable indexing, and improve joins."
                    });
                }
            }
        }

        // ---------------- RULE 2 ----------------
        private void CheckMissingIndexes(MesDbProfile profile, MesHealthReport report)
        {
            foreach (var table in profile.Tables)
            {
                if (table.Indexes.Count == 0)
                {
                    report.Issues.Add(new HealthIssue
                    {
                        Code = "NO_INDEX",
                        Severity = IssueSeverity.Warning,
                        ImpactScore = 10,
                        Description = $"Table '{table.Name}' has no indexes.",
                        Recommendation =
                            "Add at least one index on frequently filtered or joined columns."
                    });
                }
            }
        }

        // ---------------- RULE 3 ----------------
        private void CheckLargeTables(MesDbProfile profile, MesHealthReport report)
        {
            foreach (var table in profile.Tables)
            {
                if (table.RowCount >= LargeTableRowThreshold)
                {
                    report.Issues.Add(new HealthIssue
                    {
                        Code = "LARGE_TABLE",
                        Severity = IssueSeverity.Warning,
                        ImpactScore = 5,
                        Description =
                            $"Table '{table.Name}' contains {table.RowCount:N0} rows.",
                        Recommendation =
                            "Consider data archiving, partitioning, or retention policies " +
                            "to maintain MES performance."
                    });
                }
            }
        }
    }
}
