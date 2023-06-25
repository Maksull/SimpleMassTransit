using Core.Contracts.Controllers.Auth;
using Core.Mediator.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.GraphQL.Auth
{
    [ExtendObjectType("Mutation")]
    public sealed class AuthMutations
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<JwtResponse?> Login([FromServices] IMediator mediator, LoginRequest request)
        {
            return await mediator.Send(new LoginCommand(request));
        }

    }
}
