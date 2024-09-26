using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductOfferService
    {
        Task<OfferDto> GetOfferAsync(string offerType);
    }
}
