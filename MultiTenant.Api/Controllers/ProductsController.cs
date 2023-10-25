
using Microsoft.AspNetCore.Mvc;
using MultiTenant.Application.Models;
using MultiTenant.Application.Repositories;

namespace MultiTenant.Api.Controllers;
//[Authorize]
[ApiController]
[Route("{slugTenant}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MessageRepository _messageRepository;

    public ProductsController(MessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var slug = RouteData.Values["slugTenant"]?.ToString();
        return Ok(new {  });
    }

    [HttpPost]
    [Route("/message")]
    //[Authorize] 
    public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto request)
    {
        var result = await ExecuteTask(request);
        if (!result)
            return BadRequest();
        return Ok( );
    }

    private async Task<bool> ExecuteTask(CreateMessageDto request)
    {
        var delayedTime = new TimeSpan(request.Date.Ticks);
        await Task.Delay(delayedTime, new CancellationToken());
        var result = _messageRepository.SaveMessage(request);
        return true;
    }
}