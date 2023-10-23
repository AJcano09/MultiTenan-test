using FluentMigrator;
using MultiTenant.Domain.Entities;

namespace Multitenant.Infraestructure.Database.ProductByOrganization.Migrations;

[Migration(4,TransactionBehavior.Default,"CreateTableProducts")]
[Tags(tagName1:"products")]
public class AddProductTable : Migration
{
    public override void Up()
    {
        Create.Table(nameof(Product))
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("Description").AsString()
            .WithColumn("duration").AsString()
            .WithColumn("OrganizationId").AsGuid();
    }

    public override void Down()
    {
        Delete.Table(nameof(Product));
    }
}