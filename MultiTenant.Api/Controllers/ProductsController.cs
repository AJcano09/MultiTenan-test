using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenant.Application.Models;

namespace MultiTenant.Api.Controllers;

[ApiController]
[Route("{slugTenant}/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var slug = RouteData.Values["slugTenant"]?.ToString();
        // Usa el slug para determinar qué tenant es y traer sus productos
        return Ok(new {  });
    }

    [HttpPost]
    [Authorize] 
    public IActionResult CreateProduct([FromBody] ProductRequest request)
    {
        var slug = RouteData.Values["slugTenant"].ToString();
        return Ok();
    }
}