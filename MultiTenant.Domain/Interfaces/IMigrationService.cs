
namespace MultiTenant.Domain.Interfaces;

public interface IMigrationService
{
    void RunMigrationForOrganization(string? dataBaseName);
    void RunMigrationForProducts(string? tenantName);
}