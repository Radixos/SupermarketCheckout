using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef
{
    public class ItemCatalogRepository : IItemCatalogRepository
    {
        private readonly SupermarketContext _context;
        private readonly IOfferFactory _offerFactory;

        public ItemCatalogRepository(SupermarketContext context, IOfferFactory offerFactory)
        {
            _context = context 
                ?? throw new ArgumentNullException(nameof(context));
            _offerFactory = offerFactory
                ?? throw new ArgumentNullException(nameof(offerFactory));
        }

        public async Task<BasketItemPrice> GetBasketItemPriceBySkuAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            var basketItem = await _context.Product
                .Include(basketItems => basketItems.Offer)
                .FirstOrDefaultAsync(basketItem => basketItem.Sku == sku);

            if (basketItem == null)
            {
                throw new NotFoundException(nameof(basketItem));
            }

            return BasketItemMapper.MapToBasketItemPrice(basketItem, _offerFactory);
        }
    }
}
