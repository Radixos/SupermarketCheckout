using System.ComponentModel.DataAnnotations;

namespace SupermarketCheckoutAPI.DTOs
{
    public class CheckoutRequestDto
    {
        [Required]
        public string SKUs { get; set; }
    }
}
