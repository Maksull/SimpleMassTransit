using Core.Contracts.Controllers.Auth;

namespace Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        JwtResponse? Login(LoginRequest request);
    }
}
