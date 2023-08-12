using CarRental.Application.Contracts.Identity;
using CarRental.Application.Models.Auth;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Auth.Commands;

public record LoginCommand : IRequest<AuthResult>
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
{
    private readonly IAuthService authService;
    public LoginCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken) => await authService.LoginAsync(request);
}