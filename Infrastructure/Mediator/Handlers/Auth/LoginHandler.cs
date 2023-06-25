using Core.Contracts.Controllers.Auth;
using Core.Mediator.Commands.Auth;
using Infrastructure.Services.Interfaces;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Auth
{
    public sealed class LoginHandler : IRequestHandler<LoginCommand, JwtResponse?>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<JwtResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var jwt = _authService.Login(request.Request);

            return Task.FromResult(jwt);
        }
    }
}
