using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace CarRental.Api.Configuration;

internal static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var secretKey = jwtConfig["Key"];
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig["Issuer"],
                ValidAudience = jwtConfig["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Car Rental API",
                Version = "v1",
                Description = "Improved version of my old Car Rental API services",
                Contact = new OpenApiContact
                {
                    Name = "7beat",
                    Email = "t_krzem123@wp.pl",
                    Url = new("https://github.com/7beat")
                }
            });
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), includeControllerXmlComments: true);
            c.EnableAnnotations();
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
