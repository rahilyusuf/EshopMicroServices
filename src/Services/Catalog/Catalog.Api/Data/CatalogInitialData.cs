using Marten.Schema;

namespace Catalog.Api.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync(cancellation))
                return;
            //marten UPSART will cater for existing records
            session.Store(GetPreConfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        public static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
            {
                new()
                {
                    Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    Name = "IPhone X",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years.",
                    ImageUrl = "product-1.png",
                    Price = 950.00M,
                    Category = new List<string> { "Smart Phone" }
                },
                new()
                {
                    Id = new Guid("6334c996-8457-4cf0-815c-ed2b77c4ff62"),
                    Name = "Samsung Galaxy S22",
                    Description = "Latest Galaxy model with an enhanced camera and new chipset.",
                    ImageUrl = "product-2.png",
                    Price = 899.99M,
                    Category = new List<string> { "Smart Phone" }
                },
                new()
                {
                    Id = new Guid("7334c996-8457-4cf0-815c-ed2b77c4ff63"),
                    Name = "Dell XPS 15",
                    Description = "A powerful laptop with sleek design and great performance for professionals.",
                    ImageUrl = "product-3.png",
                    Price = 1499.00M,
                    Category = new List<string> { "Laptop", "Electronics" }
                },
                new()
                {
                    Id = new Guid("8334c996-8457-4cf0-815c-ed2b77c4ff64"),
                    Name = "Apple Watch Series 8",
                    Description = "Health and fitness tracking wearable with sleek design.",
                    ImageUrl = "product-4.png",
                    Price = 399.99M,
                    Category = new List<string> { "Wearables", "Smart Watch" }
                },
                new()
                {
                    Id = new Guid("9334c996-8457-4cf0-815c-ed2b77c4ff65"),
                    Name = "Sony WH-1000XM4",
                    Description = "Industry-leading noise cancelling headphones with premium sound.",
                    ImageUrl = "product-5.png",
                    Price = 349.99M,
                    Category = new List<string> { "Headphones", "Audio" }
                }
            };
    }
}
