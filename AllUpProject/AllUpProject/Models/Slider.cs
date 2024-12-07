using System.ComponentModel.DataAnnotations.Schema;

namespace AllUpProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Image {  get; set; }
        [NotMapped]
        public IFormFile? Photo {  get; set; }

  
    }
}
