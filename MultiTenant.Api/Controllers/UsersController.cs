using Microsoft.AspNetCore.Mvc;
using MultiTenant.Application.Dtos;
using MultiTenant.Application.Models;
using MultiTenant.Application.Repositories;
using MultiTenant.Domain.Interfaces;

namespace MultiTenant.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly OrganizationRepository _organizationRepository;
    private readonly ITokenService _tokenService;

    public UsersController(UserRepository userRepository, OrganizationRepository organizationRepository,ITokenService tokenService)
    {
        _userRepository = userRepository;
        _organizationRepository = organizationRepository;
        _tokenService = tokenService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        // TODO : mover logica usando mediator 
        var result = await _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);
        if (result == null)
        {
            return Unauthorized();
        }
        var organizationResult = await _organizationRepository.GetAsync(result.OrganizationId);
        var token = _tokenService.GenerateToken(result.Id.ToString(), organizationResult.Id.ToString());
        var response = new UserDto()
        {
            Data = new Data()
            {
                AccesToken = token,
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