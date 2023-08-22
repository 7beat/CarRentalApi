using CarRental.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CarRental.Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        ProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequest:
                statusCode = HttpStatusCode.BadRequest;
                problem = new()
                {
                    Title = badRequest.Message,
                    Status = (int)statusCode,
                    Detail = badRequest.InnerException?.Message,
                    Type = nameof(BadRequestException)
                };
                break;
            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new()
                {
                    Title = notFound.Message,
                    Status = (int)statusCode,
                    Detail = notFound.InnerException?.Message,
                    Type = nameof(BadRequestException)
                };
                break;
            case UnAuthorizedException unAuthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                problem = new()
                {
                    Title = unAuthorizedException.Message,
                    Status = (int)statusCode,
                    Detail = unAuthorizedException.InnerException?.Message,
                    Type = nameof(UnAuthorizedException)
                };
                break;
            default:
                problem = new()
                {
                    Title = "An unexpected error occurred.",
                    Status = (int)statusCode,
                    Detail = ex.Message,
                    Type = nameof(Exception)
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
