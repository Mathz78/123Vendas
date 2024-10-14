using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Interfaces;

namespace UmDoisTresVendas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ILogger<SalesController> _logger;

    public SalesController(ISaleService saleService, ILogger<SalesController> logger)
    {
        _saleService = saleService;
        _logger = logger;
    }

    [HttpGet("{saleId}")]
    public async Task<IActionResult> GetSale(Guid saleId)
    {
        var result = await _saleService.GetSaleByIdAsync(saleId);
        
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale(CreateSaleDto createSaleDto)
    {
        var result = await _saleService.CreateSaleAsync(createSaleDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
    
    [HttpPut("{saleId}")]
    public async Task<IActionResult> UpdateSale(Guid saleId, UpdateSaleDto updateSaleDto)
    {
        var result = await _saleService.UpdateSaleAsync(saleId, updateSaleDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPost("{saleId}/cancel")]
    public async Task<IActionResult> CancelSale(Guid saleId)
    {
        var result = await _saleService.CancelSaleAsync(saleId);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}
