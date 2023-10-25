
using MultiTenant.Api.Middleware;
using MultiTenant.Application;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure;
using Multitenant.Infraestructure.Database.ProductByOrganization.Migrations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddApplication(configuration);
builder.Services.AddInfraEstructure(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
     var migrationRunnerService = services.GetRequiredService<IMigrationService>();
   migrationRunnerService.RunMigrationForOrganization("organizations");
  // migrationRunnerService.RunMigrationForProducts();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();