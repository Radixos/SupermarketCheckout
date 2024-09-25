using System.ComponentModel.DataAnnotations;

namespace SupermarketCheckout.Repositories.Ef.Entities
{
    public class OfferEntity
    {
        public int OfferId { get; set; }
        public string OfferType { get; set; }
        public int? OfferQuantity { get; set; }
        public decimal? OfferPrice { get; set; }
        public IList<ProductEntity> BasketItems { get; set; }
    }
}
