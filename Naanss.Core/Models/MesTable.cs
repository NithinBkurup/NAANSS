namespace Naanss.Core.Models
{
    public class MesTable
    {
        public string Name { get; set; } = string.Empty;
        public long RowCount { get; set; }

        public List<MesColumn> Columns { get; set; } = new();
        public List<MesIndex> Indexes { get; set; } = new();
    }
}
