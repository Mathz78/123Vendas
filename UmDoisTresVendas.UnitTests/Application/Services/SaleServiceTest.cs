using AutoMapper;
using Moq;
using Shouldly;
using Serilog;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Interfaces;
using UmDoisTresVendas.Application.Services;
using UmDoisTresVendas.Application.Validations;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Entities.Enums;
using UmDoisTresVendas.UnitTests;

public class SaleServiceTests : BaseTest
{
    private readonly SaleService _saleService;
    private readonly Mock<ISaleRepository> _mockSaleRepository;
    private readonly SaleDtoValidator _saleDtoValidator;
    private readonly Mock<ILogger> _mockLogger;
    private readonly IMapper _mapper;

    public SaleServiceTests()
    {
        _mockSaleRepository = new Mock<ISaleRepository>();
        _saleDtoValidator = new SaleDtoValidator();
        _mockLogger = new Mock<ILogger>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateSaleDto, Sale>();
            cfg.CreateMap<SaleItemDto, SaleItem>();
        });

        _mapper = config.CreateMapper();
        _saleService = new SaleService(_mockSaleRepository.Object, _saleDtoValidator, _mockLogger.Object, _mapper);
    }
    
    [Fact]
    public async Task CreateSaleAsync_Should_Add_Sale_Successfully()
    {
        _mockSaleRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Sale>()))
            .ReturnsAsync(new Sale(ValidCreateSaleDto.CustomerId, ValidCreateSaleDto.CustomerName, ValidCreateSaleDto.BranchId, ValidCreateSaleDto.BranchName,
                                    new List<SaleItem> { new SaleItem("product-1", "Product One", 2, 50, 5) }));
        
        var result = await _saleService.CreateSaleAsync(ValidCreateSaleDto);
        
        result.ShouldNotBeNull();
        result.Success.ShouldBeTrue();
        result.Content.ShouldNotBeNull();
        result.Content.SaleId.ShouldNotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task CancelSaleAsync_Should_Cancel_Sale_Successfully()
    {
        var saleId = Guid.NewGuid();
        var sale = new Sale("customer-1", "Customer One", "branch-1", "Branch One",
            new List<SaleItem> { new SaleItem("product-1", "Product One", 2, 50, 0) });

        _mockSaleRepository.Setup(repo => repo.GetByIdAsync(saleId))
            .ReturnsAsync(sale);

        _mockSaleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Sale>()))
            .ReturnsAsync(sale);
        
        var result = await _saleService.CancelSaleAsync(saleId);
        
        result.ShouldNotBeNull();
        result.Success.ShouldBeTrue();
        result.Content.ShouldNotBeNull();
        result.Content.SaleIdentification.ShouldBe(sale.SaleIdentification);
        sale.Status.ShouldBe(SaleStatusEnum.Cancelled);
    }

    [Fact]
    public async Task CancelSaleAsync_Should_Return_Error_If_Sale_Not_Found()
    {
        var saleId = Guid.NewGuid();

        _mockSaleRepository.Setup(repo => repo.GetByIdAsync(saleId))
            .ReturnsAsync((Sale)null);
        
        var result = await _saleService.CancelSaleAsync(saleId);
        
        result.ShouldNotBeNull();
        result.Success.ShouldBeFalse();
        result.Errors.ShouldContain("No sale was found for the given Id.");
    }

    [Fact]
    public async Task CancelSaleAsync_Should_Return_Error_If_Sale_Already_Cancelled()
    {
        var saleId = Guid.NewGuid();
        var sale = new Sale("customer-1", "Customer One", "branch-1", "Branch One",
            new List<SaleItem> { new SaleItem("product-1", "Product One", 2, 50, 0) });
        sale.UpdateStatus(status: SaleStatusEnum.Cancelled);
        
        _mockSaleRepository.Setup(repo => repo.GetByIdAsync(saleId))
            .ReturnsAsync(sale);
        
        var result = await _saleService.CancelSaleAsync(saleId);
        
        result.ShouldNotBeNull();
        result.Success.ShouldBeFalse();
        result.Errors.ShouldContain("Sale is already canceled.");
    }
}
