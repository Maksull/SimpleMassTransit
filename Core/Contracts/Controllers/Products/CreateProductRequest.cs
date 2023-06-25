namespace Core.Contracts.Controllers.Products
{
    public sealed record CreateProductRequest(string Name, decimal Price, Guid CategoryId);
}
