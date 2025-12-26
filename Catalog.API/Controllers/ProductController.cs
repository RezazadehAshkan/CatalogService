using Catalog.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id }, new 
    { 
        Id = id, 
        Message = "Product created successfully"
    });
    }
}