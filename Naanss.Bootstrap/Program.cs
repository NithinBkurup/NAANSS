using Naanss.Core.Inspection;
using Naanss.Core.Evidence.Builders;
using Naanss.AI.Services;

// 1. Connection string (YOU MUST DEFINE IT)
var connectionString =
    "Server=tcp:172.16.16.19,1433;" +
    "Database=MPAS_DB;" +
    "User Id=sa;" +
    "Password=sql@2019;" +
    "Encrypt=False;" +
    "TrustServerCertificate=True;" +
    "Connection Timeout=30;";

// 2. Inspect DB
var inspector = new SqlServerMesInspector();
var profile = inspector.Inspect(connectionString);

// 3. Build evidence
var evidenceBuilder = new MesEvidenceBuilder();
var evidence = evidenceBuilder.Build(profile);

// 4. Run AI analysis (READ-ONLY)
var aiService = new AiAnalysisService();

Console.WriteLine("Running AI analysis using local LLM...");
var aiOutput = await aiService.AnalyzeAsync(evidence);

// 5. Print raw AI output (for now)
Console.WriteLine("===== AI OUTPUT (RAW) =====");
Console.WriteLine(aiOutput);
