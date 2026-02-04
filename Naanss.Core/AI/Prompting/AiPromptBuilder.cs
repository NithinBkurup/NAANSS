using System.Text;
using System.Text.Json;
using Naanss.Core.Evidence;

namespace Naanss.Core.AI.Prompting
{
    public class AiPromptBuilder
    {
        public string BuildSystemPrompt()
        {
            return """
            You are a senior Manufacturing Execution System (MES) database architect
            and SQL Server performance expert.

            You specialize in:
            - High-volume MES transactional and time-series databases
            - SQL Server indexing, statistics, and data retention
            - Industrial production environments where stability is critical

            Rules you must follow:
            - Base all conclusions strictly on the provided evidence
            - Do not assume information that is not present
            - Prefer conservative, production-safe recommendations
            - Clearly explain reasoning and trade-offs
            - If evidence is insufficient, state that explicitly
            """;
        }

        public string BuildEvidencePrompt(MesEvidence evidence)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(evidence, options);

            return $"""
            Below is structured evidence collected from an MES database and system.
            This data is factual and read-only.

            MES_EVIDENCE:
            {json}
            """;
        }

        public string BuildTaskPrompt()
        {
            return """
            Analyze the MES_EVIDENCE provided.

            For each table in the evidence:
            1. Assess alignment with SQL Server and MES best practices.
            2. Identify performance, scalability, or maintenance risks.
            3. Explain WHY each risk exists using only the evidence.
            4. Suggest SAFE, production-appropriate improvements.
            5. Explicitly state what should NOT be changed, if applicable.

            Then:
            - Assign an overall risk level (Low / Medium / High)
            - Provide a confidence score between 0.0 and 1.0 indicating how reliable
              the recommendations are based on the evidence.

            Output must be clear, structured, and professional.
            """;
        }
    }
}
