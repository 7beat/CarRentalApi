using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarRental.Infrastructure.Extensions;
public static class HealthCheckExtensions
{
    public static void AddSeq(this IHealthChecksBuilder builder)
    {
        builder.AddAsyncCheck("Seq", async () =>
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:8081/health");
            return response.IsSuccessStatusCode ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        });
    }
}