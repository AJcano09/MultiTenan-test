namespace MultiTenant.Application.Models;

public class UserLoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}