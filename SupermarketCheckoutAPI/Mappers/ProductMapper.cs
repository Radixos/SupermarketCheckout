using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class ProductMapper
    {
        public static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto
            {
                Sku = product.Sku,
                Price = product.Price,
                OfferType = product.OfferType ?? null
            };
        }

        public static Product MapToProduct(ProductDto productDto)
        {
            return new Product
            {
                Sku = productDto.Sku,
                Price = productDto.Price,
                OfferType = productDto.OfferType ?? null
            };
        }

        public static ProductsResponse MapToProductsResponse(List<ProductDto> products)
        {
            var productList = products.Select(dto => new Product
            {
                Sku = dto.Sku,
                Price = dto.Price,
                OfferType = dto.OfferType
            }).ToList();

            return new ProductsResponse
            {
                Products = productList
            };
        }
    }
}
