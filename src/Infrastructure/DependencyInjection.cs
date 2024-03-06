using Infrastructure;
using Infrastructure.RabbitMq;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var postregConnectionString = configuration.GetConnectionString("MySqlConnection");
        Guard.Against.Null(postregConnectionString, message: "Connection string 'PostgresConnection' not found.");

        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //services.AddDbContext<ApplicationDbContext>((sp, options) =>
        //{
        //    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

        //    options.Usesql(postregConnectionString);
        //});

        //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services.AddScoped<ApplicationDbContextInitialiser>();

        var redisConnectionString = configuration.GetConnectionString("RedisConnection");

        Guard.Against.Null(redisConnectionString, message: "Connection string 'RedisConnection' not found.");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "SampleInstance";
        });

        var rabbitMQConfig = new RabbitMQConfig();
        configuration.GetSection("RabbitMQConfig").Bind(rabbitMQConfig);

        services.AddSingleton<IRabbitMQConnection>(sp =>
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = rabbitMQConfig.Host,
                        UserName = rabbitMQConfig.UserName,
                        Password = rabbitMQConfig.Password
                    };

                    return new RabbitMQConnection(factory);
                });
        services.AddSingleton<EventBusRabbitMQProducer>();

        //services.AddScoped<IMTQueueProducer, MTQueueProducer>();
        //services.AddHostedService<EventBusPlaceOrderPConsumer>();

        #region Helpers


        #endregion

        //services.AddAuthentication();

        //services.AddAuthorizationBuilder();

        return services;
    }
}
