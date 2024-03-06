using Application;
using Web;
//TODO: Add api version
//TODO: Add JWT
//TODO: Add Settings to appseting files
//TODO: Add Log
//TODO: Add RABBIT MQ
//TODO: Add GRPC
//TODO: Add In-Memory-Cache
//TODO: Add Distributed-Cache

ThreadPool.SetMinThreads(200, 800);

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration
    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddCors();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);


//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenLocalhost(7288, o => o.Protocols =
// HttpProtocols.Http1);
//    //Setup a HTTP / 2 endpoint without TLS.
//   options.ListenLocalhost(5050, o => o.Protocols =
//       HttpProtocols.Http2);

//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseExceptionHandler(options => { });

app.MapEndpoints();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("Custom exception handling....");
    Console.WriteLine(ex.ToString());
    Console.WriteLine(ex.InnerException?.ToString());
}


//var builder = WebApplication.CreateBuilder(args);

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
