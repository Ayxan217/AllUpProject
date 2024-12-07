using System.ComponentModel.DataAnnotations.Schema;

namespace AllUpProject.Areas.Admin.ViewModels
{
    public class SliderVM
    {
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        
        public IFormFile? Photo { get; set; }
    }
}
