using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class ProductMapper
    {
        public static Product MapToProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            return new Product
            {
                Sku = productDto.Sku,
                Price = productDto.Price,
                Offer = OfferMapper.MapToOffer(productDto.Offer)
            };
        }

        public static ProductsResponse MapToProductsResponse(List<ProductDto> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var productList = products.Select(dto => new Product
            {
                Sku = dto.Sku,
                Price = dto.Price,
                Offer = OfferMapper.MapToOffer(dto.Offer)
            }).ToList();

            return new ProductsResponse
            {
                Products = productList
            };
        }

        public static ProductDto MapToProductDto(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductDto
            {
                Sku = product.Sku,
                Price = product.Price,
                Offer = OfferMapper.MapToOfferDto(product.Offer)
            };
        }
    }
}
