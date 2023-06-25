using Core.Entities;
using MediatR;

namespace Core.Mediator.Queries.Categories
{
    public sealed record GetQueryableCategoriesQuery() : IRequest<IQueryable<Category>>;
}
