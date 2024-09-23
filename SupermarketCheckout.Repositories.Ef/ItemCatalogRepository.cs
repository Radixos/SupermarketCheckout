using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
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

        public async Task<BasketItemPrice> GetBasketItemPriceBySKUAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentOutOfRangeException(nameof(sku));
            }

            var basketItem = await _context.BasketItem
                .Include(basketItems => basketItems.Offer)
                .FirstOrDefaultAsync(basketItem => basketItem.SKU == sku);

            return BasketItemMapper.MapToBasketItemPrice(basketItem, _offerFactory);
        }
    }
}
