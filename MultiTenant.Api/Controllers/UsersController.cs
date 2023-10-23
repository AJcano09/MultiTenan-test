using Microsoft.AspNetCore.Mvc;
using MultiTenant.Application.Dtos;
using MultiTenant.Application.Models;
using MultiTenant.Application.Repositories;

namespace MultiTenant.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly OrganizationRepository _organizationRepository;

    public UsersController(UserRepository userRepository, OrganizationRepository organizationRepository)
    {
        _userRepository = userRepository;
        _organizationRepository = organizationRepository;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        // TODO : mover logica usando mediator 
        var result = await _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);
        var organizationResult = await _organizationRepository.GetAsync(result.OrganizationId);
        var response = new UserDto()
        {
            Data = new Data()
            {
                AccesToken = "acces token from db tenant",
                Tenants = new List<Tenant>()
                {
                    new Tenant() { SlugTenant = organizationResult.Name }
                }
            },
            Status = 200,
            StatusText = "POST Request successful.",
        };
        return Ok(response);
    }
}