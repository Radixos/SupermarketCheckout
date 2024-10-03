using SupermarketCheckout.API.Mappers;

namespace SupermarketCheckout.API.Tests
{
    [TestClass]
    public class CheckoutReponseMapperTests
    {
        [TestMethod]
        public void MapToCheckoutResponse_ThrowsArgumentOutOfRangeException_WhenTotalPriceIsBelowZero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                CheckoutResponseMapper.MapToCheckoutResponse(-1));
        }

        [TestMethod]
        public void MapToCheckoutResponse_CorrectlyMapsProperties()
        {
            decimal totalPrice = 100;

            var checkoutResponse =
                CheckoutResponseMapper.MapToCheckoutResponse(totalPrice);

            Assert.AreEqual(totalPrice, checkoutResponse.TotalPrice);
        }
    }
}