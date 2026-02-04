using Naanss.AI.Clients;
using Naanss.AI.Execution;
using Naanss.Core.AI.Prompting;
using Naanss.Core.Evidence;

namespace Naanss.AI.Services
{
    public class AiAnalysisService
    {
        private readonly OllamaClient _client;
        private readonly AiPromptBuilder _promptBuilder;

        public AiAnalysisService()
        {
            _client = new OllamaClient();
            _promptBuilder = new AiPromptBuilder();
        }
        private int EstimateTimeoutSeconds(int promptLength)
        {
            const int charsPerSecond = 1500; // conservative CPU estimate
            const int baseOverhead = 60;     // model + IO overhead
            const double safetyFactor = 2.5;

            var estimated = (promptLength / charsPerSecond) + baseOverhead;
            return (int)(estimated * safetyFactor);
        }
        public async Task<string> AnalyzeAsync(MesEvidence evidence)
        {
            // 1. Build prompt
            var systemPrompt = _promptBuilder.BuildSystemPrompt();
            var evidencePrompt = _promptBuilder.BuildEvidencePrompt(evidence);
            var taskPrompt = _promptBuilder.BuildTaskPrompt();

            var fullPrompt = $"""
            {systemPrompt}

            {evidencePrompt}

            {taskPrompt}
            """;

            Console.WriteLine($"AI prompt length: {fullPrompt.Length}");

            // 2. Timeout calculation
            int timeoutSeconds = EstimateTimeoutSeconds(fullPrompt.Length);
            Console.WriteLine($"AI timeout set to {timeoutSeconds}s");

            // 🔑 CTS for AI REQUEST (timeout only)
            using var requestCts = new CancellationTokenSource(
                TimeSpan.FromSeconds(timeoutSeconds));

            // 🔑 CTS for PROGRESS BAR (UI only)
            using var progressCts = new CancellationTokenSource();

            var progressTask = AiExecutionMonitor.ShowProgressAsync(
                timeoutSeconds,
                progressCts.Token);

            try
            {
                // 3. AI call (ONLY requestCts controls this)
                var result = await _client.GenerateAsync(
                    "llama3.1:8b",
                    fullPrompt,
                    requestCts.Token);

                return result;
            }
            finally
            {
                // 4. Stop progress bar ONLY
                progressCts.Cancel();
                Console.WriteLine("\rAI analysis in progress: 100%");
            }
        }
    }
}
