using AllUpProject.Models.Base;

namespace AllUpProject.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public decimal Tax { get; set; }
        public string ProductCode { get; set; }
        public bool IsAvailable { get; set; }

        //Relational
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
