using Core.Entities;
using MediatR;

namespace Core.Mediator.Queries.Categories
{
    public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<Category?>;
}
