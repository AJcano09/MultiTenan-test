
using MultiTenant.Application.Models;
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Enums;

namespace MultiTenant.Application.Repositories;

public class MessageRepository : GenericRepository<Message>
{

    public MessageRepository(IDatabaseConnectionFactory factory) 
        : base(factory.CreateConnection(DbProvider.UsersAndOrganizations.ToString()))
    {
    }

    public async Task<int> SaveMessage(CreateMessageDto request)
    {
        var message = new Message()
        {
           City = request.City,
            Date = request.Date,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LeadId = request.LeadId,
            LeadSource = request.LeadSource,
            Phone1 = request.Phone1,
            Questions = request.Questions,
            StreetAddress = request.StreetAddress,
            State = request.State,
            Service = request.Service,
            Zip = request.Zip,
        };
      var result = await  base.InsertAsync(message);

      return result;
    }
     
}