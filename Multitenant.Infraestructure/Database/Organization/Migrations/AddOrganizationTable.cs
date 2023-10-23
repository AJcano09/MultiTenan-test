using FluentMigrator;
using MultiTenant.Domain.Entities;

namespace Multitenant.Infraestructure.Database.Organization.Migrations;

[Migration(1,TransactionBehavior.Default,"AddOrganizationTable")]
public class AddOrganizationTable : Migration
{
    public override void Up()
    {
        Create.Table(nameof(MultiTenant.Domain.Entities.Organization))
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("SlugTenant").AsString();
            
            Create.Index("Idx_Unique_SlugTenant")
                .OnTable(nameof(MultiTenant.Domain.Entities.Organization))
                .OnColumn("SlugTenant")
                .Unique();
        
        Create.Table(nameof(User))
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("Email").AsString()
            .WithColumn("Password").AsString()
            .WithColumn("OrganizationId").AsGuid();
    }

    public override void Down()
    {
        Delete.Table(nameof(MultiTenant.Domain.Entities.Organization));
        Delete.Table(nameof(User));

        Delete.Index("Idx_Unique_SlugTenant").OnTable(nameof(MultiTenant.Domain.Entities.Organization));
    }
}