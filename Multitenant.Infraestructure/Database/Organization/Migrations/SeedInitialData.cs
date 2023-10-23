using FluentMigrator;
using MultiTenant.Domain.Entities;

namespace Multitenant.Infraestructure.Database.Organization.Migrations;

[Migration(202310231546)]
public class SeedInitialData : Migration
{
    public override void Up()
    {
        var organization1Guid = Guid.NewGuid();
        var user1 = Guid.NewGuid();
        var masterTenant = "master-default";
        var nameOrganization1 = "tenant master";
        
        Insert.IntoTable(nameof(Organization))
            .Row(new { Id = organization1Guid, Name = nameOrganization1,SlugTenant=masterTenant});

        Insert.IntoTable(nameof(User))
            .Row(new { Id = user1, Name = "master user",Email ="master@localdomain.com", Password = "master123" ,OrganizationId =organization1Guid});
    }

    public override void Down()
    {
    }
}