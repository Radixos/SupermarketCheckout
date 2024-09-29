using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Services
{
    public class ProductOfferService : IProductOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public ProductOfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository
                ?? throw new ArgumentNullException(nameof(offerRepository));
        }

        public async Task<OfferDto> GetOfferAsync(string sku, string offerType) //TODO ASK: What other tests?
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            if (string.IsNullOrWhiteSpace(offerType))
            {
                throw new ArgumentException(nameof(offerType));
            }

            var offer = await _offerRepository.GetOfferAsync(sku, offerType);

            if (offer == null)
            {
                throw new NotFoundException(nameof(offer));
            }

            return OfferMapper.MapToOfferDto(offer);
        }
    }
}
