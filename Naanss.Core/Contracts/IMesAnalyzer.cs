namespace Naanss.Core.Contracts
{
    using Naanss.Core.Health;
    using Naanss.Core.Models;

    public interface IMesAnalyzer
    {
        MesHealthReport Analyze(MesDbProfile profile);
    }
}
