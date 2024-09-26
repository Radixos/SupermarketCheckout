using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.Application.Services
{
    public interface IOfferService
    {
        Task<OfferDto> GetOfferAsync(string offerType);
    }
}
