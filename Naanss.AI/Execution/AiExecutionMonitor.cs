namespace Naanss.AI.Execution
{
    public static class AiExecutionMonitor
    {
        public static Task ShowProgressAsync(
            int estimatedSeconds,
            CancellationToken token)
        {
            return Task.Run(async () =>
            {
                for (int i = 0; i <= estimatedSeconds; i++)
                {
                    if (token.IsCancellationRequested)
                        break;

                    int percent = Math.Min(95, (i * 100) / estimatedSeconds);
                    Console.Write($"\rAI analysis in progress: {percent}%");
                    await Task.Delay(1000, token);
                }
            }, token);
        }
    }
}
