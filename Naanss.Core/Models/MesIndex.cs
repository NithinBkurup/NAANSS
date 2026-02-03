namespace Naanss.Core.Models
{
    public class MesIndex
    {
        public string Name { get; set; } = string.Empty;
        public bool IsClustered { get; set; }
        public bool IsUnique { get; set; }
        public List<string> Columns { get; set; } = new();
    }
}
