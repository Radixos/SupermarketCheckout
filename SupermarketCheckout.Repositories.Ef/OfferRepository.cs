using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef
{
    public class OfferRepository : IOfferRepository
    {
        private readonly SupermarketContext _context;
        private readonly IOfferFactory _offerFactory;

        public OfferRepository(SupermarketContext context, IOfferFactory offerFactory)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
            _offerFactory = offerFactory
                ?? throw new ArgumentNullException(nameof(offerFactory));
        }

        public async Task<Offer?> GetOfferAsync(string sku, string offerType)    //TODO ASK: Where is the place for all of those tests?
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            if (string.IsNullOrWhiteSpace(offerType))
            {
                throw new ArgumentException(nameof(offerType));
            }

            var productWithOffer = await _context.Product
                .Where(p => p.Sku == sku && p.Offer != null)
                .Join(_context.Offer,
                    product => product.OfferId,
                    offer => offer.OfferId,
                    (product, offer) => new { product, offer })
                .FirstOrDefaultAsync(joined => joined.offer.OfferType == offerType);

            if (productWithOffer == null)
            {
                throw new NotFoundException(nameof(productWithOffer));
            }

            var offer = BasketItemMapper.MapToOffer(productWithOffer.offer, _offerFactory);    //TODO ASK: is this how I should map if I use offerFactory?

            return offer;
        }
    }
}
