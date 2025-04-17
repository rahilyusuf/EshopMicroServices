namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id{ get; set; } //GUID: Globally Unique Identifier
        public String Name { get; set; } = default!;
        public List<String> Category { get; set; } = new();
        public String Description { get; set; } = default!;
        public String ImageUrl { get; set; } = default!; 
        public Decimal Price { get; set; }
    }
}
