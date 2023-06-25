using Core.Entities;
using MediatR;

namespace Core.Mediator.Commands.Categories
{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Category?>;
}
