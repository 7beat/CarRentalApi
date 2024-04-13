using Asp.Versioning;
using CarRental.Application.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.V2;

public static class CarsEndpoints
{
    public static void MapCarsEndpoints(this IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        var groupBuilder = app.MapGroup("api/v{apiVersion:apiVersion}/cars")
            .WithApiVersionSet(apiVersionSet).WithOpenApi()
            .WithTags("Cars")
            .WithOpenApi();

        groupBuilder.MapGet("test", async (
            [FromServices] CarsApi module) =>
        {
            return await module.TestAsync();
        })
        .WithName("Testing");

        groupBuilder.MapPost("add", async (
            AddCarRequest request,
            HttpContext httpContext,
            HttpRequest httpRequest,
            [FromServices] CarsApi module) => /*await module.AddCarAsync(request, httpRequest.Headers.Authorization.ToString())*/
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            return await module.AddCarAsync(request, httpRequest.Headers.Authorization.ToString().Split(" ").Last());
        })
        .RequireAuthorization(opt => opt.RequireRole("Admin"));
    }
}