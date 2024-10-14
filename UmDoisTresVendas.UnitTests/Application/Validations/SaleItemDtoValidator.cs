using FluentValidation.TestHelper;
using Shouldly;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Validations;

namespace UmDoisTresVendas.UnitTests.Application.Validations
{
    public class SaleItemDtoValidatorTests
    {
        private readonly SaleItemDtoValidator _validator;

        public SaleItemDtoValidatorTests()
        {
            _validator = new SaleItemDtoValidator();
        }

        [Theory]
        [MemberData(nameof(ValidSaleItemDtoData))]
        public void Should_Not_Have_Any_Validation_Errors_When_SaleItemDto_Is_Valid(
            SaleItemDto saleItemDto)
        {
            var result = _validator.TestValidate(saleItemDto);
            result.IsValid.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(InvalidSaleItemDtoData))]
        public void Should_Have_Error_When_SaleItemDto_Is_Invalid(
            SaleItemDto saleItemDto, 
            string expectedField, 
            string expectedMessage)
        {
            var result = _validator.TestValidate(saleItemDto);
            var validationError = result.Errors
                .FirstOrDefault(x => x.PropertyName == expectedField);

            validationError.ShouldNotBeNull();
            validationError.ErrorMessage.ShouldBe(expectedMessage);
        }

        public static IEnumerable<object[]> ValidSaleItemDtoData()
        {
            yield return new object[] 
            { 
                new SaleItemDto 
                {
                    ProductId = "product-7", 
                    Quantity = 1, 
                    UnitPrice = 100, 
                    Discount = 0
                } 
            };
            yield return new object[] 
            { 
                new SaleItemDto 
                {
                    ProductId = "product-3", 
                    Quantity = 3, 
                    UnitPrice = 773, 
                    Discount = 20
                } 
            };
        }

        public static IEnumerable<object[]> InvalidSaleItemDtoData()
        {
            yield return new object[] 
            { 
                new SaleItemDto 
                { 
                    ProductId = "", 
                    Quantity = 1, 
                    UnitPrice = 100.00m, 
                    Discount = 0.00m 
                }, 
                "ProductId", 
                "ProductId cannot be null or empty." 
            };
            yield return new object[] 
            { 
                new SaleItemDto 
                { 
                    ProductId = "p-1", 
                    Quantity = 0, 
                    UnitPrice = 333, 
                    Discount = 0 
                }, 
                "Quantity", 
                "Quantity must be greater than zero." 
            };
            yield return new object[] 
            { 
                new SaleItemDto 
                { 
                    ProductId = "1", 
                    Quantity = 1, 
                    UnitPrice = 0, 
                    Discount = 0 
                }, 
                "UnitPrice", 
                "UnitPrice must be greater than zero." 
            };
            yield return new object[] 
            { 
                new SaleItemDto 
                { 
                    ProductId = "3", 
                    Quantity = 1, 
                    UnitPrice = 100, 
                    Discount = -1 
                }, 
                "Discount", 
                "Discount must be zero or greater." 
            };
        }
    }
}
