﻿namespace SupermarketCheckout.Model.Repositories
{
    public interface IOfferRepository
    {
        Task<Offer> GetOfferAsync(string offerType);
    }
}
