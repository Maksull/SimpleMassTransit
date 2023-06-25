namespace Core.Contracts.Consumers.Products
{
    public sealed record ProductUpdated(Guid ProductId, string Name, decimal Price, Guid CategoryId);
}
