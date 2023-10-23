
using MultiTenant.Api.Middleware;
using MultiTenant.Application;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// Inicializa Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Agregar Serilog al pipeline
builder.Host.UseSerilog();

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfraEstructure(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // var runner = services.GetRequiredService<IMigrationRunner>();
    // runner.MigrateUp();
     var migrationRunnerService = services.GetRequiredService<IMigrationService>();
   migrationRunnerService.RunMigrationForOrganization("organizations");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseMiddleware<TenantMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();