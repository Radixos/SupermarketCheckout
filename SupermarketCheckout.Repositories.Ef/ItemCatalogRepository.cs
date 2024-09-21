using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef
{
    public class ItemCatalogRepository : IItemCatalogRepository
    {
        private readonly SupermarketContext _context;

        public ItemCatalogRepository(SupermarketContext context)
        {
            _context = context 
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BasketItemPrice> GetBasketItemPriceBySKUAsync(string sku)   //TODO: fix
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentOutOfRangeException(nameof(sku));
            }

            var basketItem = await _context.BasketItem
                .Include(basketItems => basketItems.Offer)
                .FirstOrDefaultAsync(basketItem => basketItem.SKU == sku);

            return BasketItemMapper.MapToBasketItemPrice(basketItem);
        }
    }
}
