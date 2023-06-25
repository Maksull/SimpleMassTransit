namespace Core.Contracts.Consumers.Products
{
    public sealed record ProductDeleted(Guid ProductId, string Name, decimal Price, Guid CategoryId);
}
