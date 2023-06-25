using Core.Entities;
using MediatR;

namespace Core.Mediator.Queries.Products
{
    public sealed record GetProductByIdQuery(Guid Id) : IRequest<Product?>;
}
