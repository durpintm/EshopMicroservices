using Ordering.API.Abstractions;

namespace Ordering.API.Models;
public class Product : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}

