using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketCheckout.Repositories.Ef.Entities
{
    public class ProductEntity
    {
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? OfferId { get; set; }
        public OfferEntity? Offer { get; set; }
    }
}
