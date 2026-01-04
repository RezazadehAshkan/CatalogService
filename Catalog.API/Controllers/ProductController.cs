using Catalog.Application.Commands.CreateProduct;
using Catalog.Application.Queries;
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


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
        {
            return NotFound(new { Message = "Product not found" });
        }
        return Ok(product);
    }   
}