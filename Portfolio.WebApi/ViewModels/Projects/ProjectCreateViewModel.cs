using Portfolio.WebApi.Commons.Attributes;
using Portfolio.WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.WebApi.ViewModels.Projects
{
    public class ProjectCreateViewModel
    {
        [Required(ErrorMessage = "ProjName is required")]
        [MaxLength(35), MinLength(2)]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "ProgLanguage is required")]
        [MaxLength(35), MinLength(2)]
        public string ProgLanguage { get; set; } = String.Empty;

        [Required(ErrorMessage = "TechnologiesUsed is required")]
        [MaxLength(35), MinLength(2)]
        public string TechnologiesUsed { get; set; } = String.Empty;

        [Required(ErrorMessage = "ResursCodeni sselkasini kiriting")]
        public string ResursCode { get; set; } = String.Empty;

        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [MaxFileSize(3)]
        [AllowedFileExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; } = null!;

        public string Description { get; set; } = String.Empty;
    }
}
