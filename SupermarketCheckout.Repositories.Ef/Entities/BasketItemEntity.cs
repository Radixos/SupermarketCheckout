﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketCheckout.Repositories.Ef.Entities
{
    public class BasketItemEntity
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int? OfferId { get; set; }
        public OfferEntity? Offer { get; set; }
    }
}
