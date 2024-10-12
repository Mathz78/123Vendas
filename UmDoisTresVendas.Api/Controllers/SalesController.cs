using Microsoft.AspNetCore.Mvc;

namespace UmDoisTresVendas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ILogger<SalesController> _logger;

    public SalesController(ILogger<SalesController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult GetSale(int id)
    {
        return Ok("Hello World!");
    }
}