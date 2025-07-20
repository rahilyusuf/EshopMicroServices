
namespace Ordering.Domain.Models
{
    class Customer:Entity<Guid>
    {
        public string name { get; private set; } = default!;
        public string email { get; private set;} = default!;
    }
}
