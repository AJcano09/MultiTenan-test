namespace MultiTenant.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email  { get; set; }
    public string Password { get; set; }
    public Guid OrganizationId { get; set; }
}