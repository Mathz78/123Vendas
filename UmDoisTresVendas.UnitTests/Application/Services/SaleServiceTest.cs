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
        // Arrenge
        _mockSaleRepository
            .Setup(repo => repo.AddAsync(It.IsAny<Sale>()))
            .ReturnsAsync(new Sale(ValidCreateSaleDto.CustomerId, ValidCreateSaleDto.CustomerName, ValidCreateSaleDto.BranchId, ValidCreateSaleDto.BranchName,
                                    new List<SaleItem> { new SaleItem("product-1", "Product One", 2, 50, 5) }));

        // Act
        var result = await _saleService.CreateSaleAsync(ValidCreateSaleDto);

        // Assert
        result.ShouldNotBeNull();
        result.Success.ShouldBeTrue();
        result.Content.ShouldNotBeNull();
        result.Content.SaleId.ShouldNotBeNullOrEmpty();
    }
}
