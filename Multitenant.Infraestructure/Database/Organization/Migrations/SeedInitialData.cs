using FluentMigrator;
using MultiTenant.Domain.Entities;

namespace Multitenant.Infraestructure.Database.Organization.Migrations;

[Migration(2)]
public class SeedInitialData : Migration
{
    public override void Up()
    {
        var organization1Guid = Guid.NewGuid();
        var user1 = Guid.NewGuid();
        var nameOrganization1 = "master";
        
        Insert.IntoTable(nameof(Organization))
            .Row(new { Id = organization1Guid, Name = nameOrganization1 });

        Insert.IntoTable(nameof(User))
            .Row(new { Id = user1, Name = "master user", Password = "master123" ,OrganizationId =organization1Guid});
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}