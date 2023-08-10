namespace CarRental.Application.Models.Auth;

public class AuthResult
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }

    public bool Succeded { get; init; }
}
