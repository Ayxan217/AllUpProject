using AllUpProject.Models.Base;

namespace AllUpProject.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
