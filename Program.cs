using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsFunctions.Models.School;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    .AddDbContext<SchoolContext>(options =>
    {
        var connectionString = Environment.GetEnvironmentVariable("DatabaseConnection");
        options.UseSqlServer(connectionString);
    })
    .AddSingleton<HttpClient>();

builder.Build().Run();
