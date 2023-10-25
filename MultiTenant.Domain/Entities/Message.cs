namespace MultiTenant.Domain.Entities;

public class Message
{
    public int id { get; set; }
    
    public DateTime Date { get; set; }

    public string LeadId { get; set; }

    public string  FirstName { get; set; }

    public string LastName { get; set; }

    public string StreetAddress { get; set; }

    public string City { get; set; }

    public string Zip { get; set; }

    public string State { get; set; }

    public string Phone1 { get; set; }

    public string Email { get; set; }

    public string LeadSource { get; set; }

    public string Service { get; set; }

    public string Questions { get; set; }
}