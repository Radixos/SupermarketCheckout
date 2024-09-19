namespace SupermarketCheckout.Application.Services
{
    public interface ICheckoutService
    {
        Task<decimal> GetTotalPriceAsync(string SKUs);
    }
}
