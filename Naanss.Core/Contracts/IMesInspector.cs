namespace Naanss.Core.Contracts
{
    using Naanss.Core.Models;

    public interface IMesInspector
    {
        MesDbProfile Inspect(string connectionString);
    }
}
