using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.ViewModels.Projects
{
    public class ProjectViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string ProgLanguage { get; set; } = String.Empty;
        public string TechnologiesUsed { get; set; } = String.Empty;
        public string ResursCode { get; set; } = String.Empty;
        public string LogoPath { get; set; } = String.Empty;
        public long UserId { get; set; }
        public virtual User? User { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}
