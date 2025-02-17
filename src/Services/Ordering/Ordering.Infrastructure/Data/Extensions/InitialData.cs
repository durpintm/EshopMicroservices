namespace Ordering.Infrastructure.Data.Extensions;
internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>()
    {
    Customer.Create(CustomerId.Of(new Guid("E7395BC0-C57F-4159-A818-99B7B8146FB6")), "durpin", "durpinthapa@gmail.com"),
    Customer.Create(CustomerId.Of(new Guid("774E2FFE-246C-498D-8D92-1031F7690B7B")), "sushil", "sushilaryal@gmail.com"),
    };
}

