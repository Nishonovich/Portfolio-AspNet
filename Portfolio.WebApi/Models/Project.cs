using Portfolio.WebApi.Commons;

namespace Portfolio.WebApi.Models
{
    public class Project : Auditable
    {
        public string Name { get; set; } = String.Empty;
        public string ProgLanguage { get; set; } = String.Empty;
        public string TechnologiesUsed { get; set; } = String.Empty;  
        public string ResursCode { get; set; } = String.Empty;
        public string LogoPath { get; set; } = String.Empty;
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public string Description { get; set; } = String.Empty;
    }
}
