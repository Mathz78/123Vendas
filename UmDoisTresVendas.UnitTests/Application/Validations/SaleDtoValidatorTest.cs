using FluentValidation.TestHelper;
using Shouldly;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Validations;

namespace UmDoisTresVendas.Tests.Application.Validations
{
    public class SaleDtoValidatorTests
    {
        private readonly SaleDtoValidator _validator;

        public SaleDtoValidatorTests()
        {
            _validator = new SaleDtoValidator();
        }

        [Theory]
        [MemberData(nameof(InvalidSaleDtoData))]
        public void Should_Have_Error_When_SaleDto_Is_Invalid(CreateSaleDto saleDto, string expectedField, string expectedMessage)
        {
            // Act
            var result = _validator.TestValidate(saleDto);

            // Assert
            result.ShouldHaveValidationErrorFor(expectedField)
                .WithErrorMessage(expectedMessage);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Validation_Is_Successful()
        {
            // Arrange
            var saleDto = new CreateSaleDto
            {
                CustomerId = "customer-1",
                CustomerName = "John Doe",
                BranchId = "branch-1",
                BranchName = "Main Branch",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductId = "product-1",
                        Quantity = 1,
                        UnitPrice = 10,
                        Discount = 1
                    }
                }
            };

            // Act
            var result = _validator.TestValidate(saleDto);

            // Assert
            result.IsValid.ShouldBe(true);
        }
        
        public static IEnumerable<object[]> InvalidSaleDtoData()
        {
            yield return new object[]
            {
                new CreateSaleDto
                {
                    CustomerId = "",
                    CustomerName = "John Doe",
                    BranchId = "branch-1",
                    BranchName = "Main Branch",
                    Items = new List<SaleItemDto> { new SaleItemDto { ProductId = "product-1", Quantity = 1, UnitPrice = 10 } }
                },
                nameof(CreateSaleDto.CustomerId),
                "The field CustomerId cannot be null or empty."
            };

            yield return new object[]
            {
                new CreateSaleDto
                {
                    CustomerId = "customer-1",
                    CustomerName = "",
                    BranchId = "branch-1",
                    BranchName = "Main Branch",
                    Items = new List<SaleItemDto> { new SaleItemDto { ProductId = "product-1", Quantity = 1, UnitPrice = 10 } }
                },
                nameof(CreateSaleDto.CustomerName),
                "The field CustomerName cannot be null or empty."
            };

            yield return new object[]
            {
                new CreateSaleDto
                {
                    CustomerId = "customer-1",
                    CustomerName = "John Doe",
                    BranchId = "",
                    BranchName = "Main Branch",
                    Items = new List<SaleItemDto> { new SaleItemDto { ProductId = "product-1", Quantity = 1, UnitPrice = 10 } }
                },
                nameof(CreateSaleDto.BranchId),
                "The field BranchId cannot be null or empty."
            };

            yield return new object[]
            {
                new CreateSaleDto
                {
                    CustomerId = "customer-1",
                    CustomerName = "John Doe",
                    BranchId = "branch-1",
                    BranchName = "",
                    Items = new List<SaleItemDto> { new SaleItemDto { ProductId = "product-1", Quantity = 1, UnitPrice = 10 } }
                },
                nameof(CreateSaleDto.BranchName),
                "The field BranchName cannot be null or empty."
            };

            yield return new object[]
            {
                new CreateSaleDto
                {
                    CustomerId = "customer-1",
                    CustomerName = "John Doe",
                    BranchId = "branch-1",
                    BranchName = "Main Branch",
                    Items = null
                },
                nameof(CreateSaleDto.Items),
                "Items list cannot be null."
            };

            yield return new object[]
            {
                new CreateSaleDto
                {
                    CustomerId = "customer-1",
                    CustomerName = "John Doe",
                    BranchId = "branch-1",
                    BranchName = "Main Branch",
                    Items = new List<SaleItemDto>() // Empty list
                },
                nameof(CreateSaleDto.Items),
                "Items list must contain at least one item."
            };
        }
    }
}
