using Core.CustomException;
using Core.Model;
using Core.Service;
using Core.Wrapper;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class PaymentTest
    {
        private PaymentService paymentService;
        private readonly string AMOUNT_PROVIDED_EXCEPTION = "The amount provided must be greater or equal to amount total!";
        private List<Bill> bills = new List<Bill>();
        private List<Coin> coins = new List<Coin>();
        private List<Item> items = new List<Item>();        

        public PaymentTest()
        {
            items.Add(new Item(10.00M));
            items.Add(new Item(20.00M));            
        }        

        [Fact]
        public void IsValidPaymentServiceInstance()
        {
            paymentService = new PaymentService(new Config());
            Assert.NotNull(paymentService);
        }

        [Fact]
        public void NotValidAmount()
        {                                                         
            var ticket = new Ticket(items);
            ticket.AmountProvided = new AmountProvided(bills, coins);

            paymentService = new PaymentService(new Config());
            var amountProvidedException = Assert.Throws<AmountProvidedException>(() => paymentService.IsValidAmount(ticket));
            Assert.Equal(AMOUNT_PROVIDED_EXCEPTION, amountProvidedException.Message);
        }

        [Fact]
        public void ValidAmount()
        {
            bills.Add(new Bill(100.00M));
            var ticket = new Ticket(items);
            ticket.AmountProvided = new AmountProvided(bills, coins);

            paymentService = new PaymentService(new Config());
            var response = paymentService.IsValidAmount(ticket);
            Assert.True(response);
        }

        [Fact]
        public void CalculateAmountDue()
        {

            bills.Add(new Bill(20.00M));
            bills.Add(new Bill(10.00M));

            var ticket = new Ticket(items);
            ticket.AmountProvided = new AmountProvided(bills, coins);

            paymentService = new PaymentService(new Config());
            var isValidAmount = paymentService.IsValidAmount(ticket);
            Assert.True(isValidAmount);

            paymentService.CalculateAmountDue(ticket);
            Assert.StrictEqual(ticket.AmountDue.Bills.Count, ticket.AmountDue.Coins.Count);

            coins.Add(new Coin(0.50M));
            ticket.AmountProvided = new AmountProvided(bills, coins);

            isValidAmount = paymentService.IsValidAmount(ticket);
            Assert.True(isValidAmount);

            var denominationsUS = "0.01, 0.05, 0.10, 0.25, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100.00".Split(",");
            Mock<IConfig> mockConfig = new Mock<IConfig>();
            mockConfig.Setup(m => m.language).Returns("US");
            mockConfig.Setup(m => m.denominationsUS).Returns(denominationsUS);

            paymentService = new PaymentService(mockConfig.Object);
            paymentService.CalculateAmountDue(ticket);
            Assert.Equal(0, ticket.AmountDue.Bills.Count);
            Assert.Equal(1, ticket.AmountDue.Coins.Count);

        }

    }
}
