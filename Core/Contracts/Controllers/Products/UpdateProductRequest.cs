namespace Core.Contracts.Controllers.Products
{
    public sealed record UpdateProductRequest(Guid ProductId, string Name, decimal Price, Guid CategoryId);
}
