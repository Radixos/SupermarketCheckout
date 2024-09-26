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

        public async Task<Offer> GetOfferAsync(string offerType)
        {
            if (string.IsNullOrWhiteSpace(offerType))
            {
                throw new ArgumentException(nameof(offerType));
            }

            var offerEntity = await _context.Offer.FirstOrDefaultAsync(o => o.OfferType == offerType);

            if (offerEntity == null)
            {
                throw new NotFoundException(nameof(offerEntity));
            }

            var offer = BasketItemMapper.MapToOffer(offerEntity, _offerFactory);

            if (offer == null)
            {
                throw new NotFoundException(nameof(offer));
            }

            return offer;
        }
    }
}
