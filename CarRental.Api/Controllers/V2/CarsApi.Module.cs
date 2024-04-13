using CarRental.Application.Contracts.Requests;
using CarRental.Application.Features.Cars.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarRental.Api.Controllers.V2;

public partial class CarsApi
{
    public async Task<IResult> TestAsync()
    {
        return Results.Ok("Worked from module!");
    }

    public async Task<IResult> AddCarAsync(AddCarRequest request, string token)
    {
        var command = mapper.Map<AddCarCommand>(request);
        command.CreatedBy = GetUserId(token);
        var id = await mediator.Send(command);
        return Results.Ok(id);
    }

    private Guid GetUserId(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        var userId = jsonToken!.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        return Guid.Parse(userId);
    }
}
