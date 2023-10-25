namespace MultiTenant.Application.Models;

public class CreateMessageDto
{
    public DateTime Date { get; set; }

    public string LeadId { get; set; }

    public string  FirstName { get; set; }

    public string LastName { get; set; }

    public string StreetAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Phone1 { get; set; }

    public string Email { get; set; }

    public string LeadSource { get; set; }

    public string Service { get; set; }

    public string Questions { get; set; }
    
    public string Zip { get; set; }
}

/*
 {
   "Date/Time": "09/08/2023 12:34",
   "LeadID": "52420eb21f1799de",
   "FirstName": "(TEST LEAD)",
   "LastName": "TestConnect",
   "StreetAddress": "123 Lead Street",
   "City": "Columbus",
   "State": "MD",
   "Zip": "43215",
   "Phone1": "111111",
   "Email": "jsmith@abcd.com",
   "LeadSource": "tesging.com",
   "Service": "Roofing",
   "Questions": "This is Test?"
   }
 */