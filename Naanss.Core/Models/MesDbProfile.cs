namespace Naanss.Core.Models
{
    public class MesDbProfile
    {
        public string DatabaseName { get; set; } = string.Empty;
        public DateTime CapturesAt { get; set; } = DateTime.UtcNow;

        public List<MesTable> Tables { get; set; } = new();
        public List<MesRelationship> Relationships { get; set; } = new();
    }
}