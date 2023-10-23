
using MultiTenant.Domain.Entities;
using MultiTenant.Domain.Interfaces;
using Multitenant.Infraestructure.Enums;

namespace MultiTenant.Application.Repositories;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(IDatabaseConnectionFactory factory)
        : base(factory.CreateConnection(DbProvider.Products.ToString()))
    {
    }
}