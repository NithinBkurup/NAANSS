using System.Net.Http.Json;
using System.Text.Json;

namespace Naanss.AI.Clients
{
    public class OllamaClient
    {
        private readonly HttpClient _http;

        public OllamaClient()
        {
            _http = new HttpClient
            {
                Timeout = Timeout.InfiniteTimeSpan
            };
        }

        public async Task<string> GenerateAsync(
            string model,
            string prompt,
            CancellationToken cancellationToken)
        {
            var payload = new
            {
                model = model,
                prompt = prompt,
                stream = false
            };

            var response = await _http.PostAsJsonAsync(
                "http://127.0.0.1:11434/api/generate",
                payload);

            response.EnsureSuccessStatusCode();

            using var doc = JsonDocument.Parse(
                await response.Content.ReadAsStringAsync());

            return doc.RootElement
                    .GetProperty("response")
                    .GetString()!;

        }
    }
}
