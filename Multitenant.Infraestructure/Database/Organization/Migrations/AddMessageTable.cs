using FluentMigrator;
using MultiTenant.Domain.Entities;

namespace Multitenant.Infraestructure.Database.Organization.Migrations;
[Migration(202310231547,TransactionBehavior.Default,"AddMessageTable")]
public class AddMessageTable : Migration
{
    public override void Up()
    {
        Create.Table(nameof(Message))
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("Date").AsDate()
            .WithColumn("LeadId").AsString()
            .WithColumn("FirstName").AsString()
            .WithColumn("LastName").AsString()
            .WithColumn("StreetAddress").AsString()
            .WithColumn("City").AsString()
            .WithColumn("Zip").AsString()
            .WithColumn("State").AsString()
            .WithColumn("Phone1").AsString()
            .WithColumn("Email").AsString()
            .WithColumn("LeadSource").AsString()
            .WithColumn("Serice").AsString()
            .WithColumn("Questions").AsString();
    }

    public override void Down()
    {
        Delete.Table(nameof(Message));
    }
}