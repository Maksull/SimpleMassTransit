using Core.Entities;
using MediatR;

namespace Core.Mediator.Queries.Products
{
    public sealed record GetQueryableProductsQuery() : IRequest<IQueryable<Product>>;
}
