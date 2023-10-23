namespace MultiTenant.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name{ get; set; }
    public string Description { get; set; }
    public string duration{ get; set; }
    public Guid OrganizationId { get; set; }
}