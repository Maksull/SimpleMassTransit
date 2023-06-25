namespace Core.Contracts.Consumers.Products
{
    public sealed record ProductCreated(Guid ProductId, string Name, decimal Price, Guid CategoryId);
}
