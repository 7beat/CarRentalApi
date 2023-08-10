using CarRental.Application.Features.Auth.Commands;
using CarRental.Application.Models.Auth;

namespace CarRental.Application.Contracts.Identity;
public interface IAuthService
{
    Task<AuthResult> LoginAsync(LoginCommand request);
    Task<AuthResult> RegisterAsync(RegisterCommand request);
}
