using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
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

        public async Task<OfferDto> GetOfferAsync(string sku, string offerType)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku));
            }

            if (string.IsNullOrWhiteSpace(offerType))
            {
                throw new ArgumentException(nameof(offerType));
            }

            var offer = await _offerRepository.GetOfferAsync(sku, offerType);

            return OfferMapper.MapToOfferDto(offer);
        }
    }
}
