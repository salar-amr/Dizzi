using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Web.Services;
using ZymLabs.NSwag.FluentValidation;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration);

        //services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        //services.AddHealthChecks()
        //    .AddDbContextCheck<ApplicationDbContext>();

        //services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddScoped(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Dizzi",
                Description = "Dizzi service",
                TermsOfService = new Uri("https://www.pindis.com")
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter the token in the field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
             });
        });

        return services;
    }
}
