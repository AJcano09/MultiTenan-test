namespace MultiTenant.Domain.Interfaces;

public interface ITokenService
{
    string GenerateToken(string userId, string tenantId);
}