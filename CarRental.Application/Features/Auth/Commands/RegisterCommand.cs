using CarRental.Application.Contracts.Identity;
using CarRental.Application.Models.Auth;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Features.Auth.Commands;
public record RegisterCommand : IRequest<AuthResult>
{
    [Required]
    [EmailAddress]
    public string Email { get; init; }
    [Required]
    public string Username { get; init; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; }
    [Required]
    public string FirstName { get; init; }
    [Required]
    public string LastName { get; init; }
    [Required]
    [DataType(DataType.Date)]
    public DateOnly Birthday { get; init; }
}

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
{
    private readonly IAuthService authService;
    public RegisterCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await authService.RegisterAsync(request);
    }
}