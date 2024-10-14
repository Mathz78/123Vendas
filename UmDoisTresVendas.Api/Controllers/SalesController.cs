using Microsoft.AspNetCore.Mvc;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Interfaces;

namespace UmDoisTresVendas.Api.Controllers;

/// <summary>
/// Controller for managing sales operations.
/// Provides endpoints for creating, retrieving, updating, and canceling sales.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly ILogger<SalesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalesController"/> class.
    /// </summary>
    /// <param name="saleService">The service used for sale operations.</param>
    /// <param name="logger">The logger for logging actions in the controller.</param>
    public SalesController(ISaleService saleService, ILogger<SalesController> logger)
    {
        _saleService = saleService;
        _logger = logger;
    }

    /// <summary>
    /// Gets the details of a specific sale.
    /// </summary>
    /// <param name="saleId">GUID Sale ID</param>
    /// <returns>Returns an <see cref="IActionResult"/> containing the sale details or an error message.</returns>
    [HttpGet("{saleId}")]
    public async Task<IActionResult> GetSale(Guid saleId)
    {
        _logger.LogInformation("Received request to get sale with ID: {SaleId}", saleId);
    
        var result = await _saleService.GetSaleByIdAsync(saleId);
    
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="createSaleDto">Sale DTO</param>
    /// <returns>Returns an <see cref="IActionResult"/> containing the result of the create operation.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateSale(CreateSaleDto createSaleDto)
    {
        _logger.LogInformation("Received request to create a sale: {@CreateSaleDto}", createSaleDto);
    
        var result = await _saleService.CreateSaleAsync(createSaleDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }
    
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="saleId">GUID Sale ID</param>
    /// <param name="updateSaleDto">DTO with updated sale details</param>
    /// <returns>Returns an <see cref="IActionResult"/> containing the result of the update operation.</returns>
    [HttpPut("{saleId}")]
    public async Task<IActionResult> UpdateSale(Guid saleId, UpdateSaleDto updateSaleDto)
    {
        _logger.LogInformation("Received request to update sale with ID: {SaleId}. Data: {@UpdateSaleDto}", saleId, updateSaleDto);
    
        var result = await _saleService.UpdateSaleAsync(saleId, updateSaleDto);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    /// <summary>
    /// Cancels a sale.
    /// </summary>
    /// <param name="saleId">GUID Sale ID</param>
    /// <returns>Returns an <see cref="IActionResult"/> containing the result of the cancel operation.</returns>
    [HttpPost("{saleId}/cancel")]
    public async Task<IActionResult> CancelSale(Guid saleId)
    {
        _logger.LogInformation("Received request to cancel sale with ID: {SaleId}", saleId);
    
        var result = await _saleService.CancelSaleAsync(saleId);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}
