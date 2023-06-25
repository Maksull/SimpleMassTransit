using Core.Contracts.Controllers.Auth;
using MediatR;

namespace Core.Mediator.Commands.Auth
{
    public sealed record LoginCommand(LoginRequest Request) : IRequest<JwtResponse?>;
}
