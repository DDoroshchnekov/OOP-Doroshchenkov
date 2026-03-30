using System;
using Moq;
using Xunit;
using lab31v1;

namespace lab31v1.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _emailServiceMock.Object);
        }

        [Fact]
        public void ProcessOrder_ValidOrder_SavesOrder()
        {
            var order = new Order
            {
                Id = 1,
                CustomerEmail = "test@example.com",
                TotalAmount = 100,
                IsProcessed = false
            };

            _orderRepositoryMock.Setup(r => r.GetById(1)).Returns(order);

            _orderService.ProcessOrder(1);

            _orderRepositoryMock.Verify(r => r.Save(It.Is<Order>(o => o.IsProcessed == true)), Times.Once);
        }

        [Fact]
        public void ProcessOrder_ValidOrder_SendsEmail()
        {
            var order = new Order
            {
                Id = 2,
                CustomerEmail = "client@example.com",
                TotalAmount = 200,
                IsProcessed = false
            };

            _orderRepositoryMock.Setup(r => r.GetById(2)).Returns(order);

            _orderService.ProcessOrder(2);

            _emailServiceMock.Verify(
                e => e.SendEmail(
                    "client@example.com",
                    "Order processed",
                    It.Is<string>(body => body.Contains("2"))
                ),
                Times.Once
            );
        }

        [Fact]
        public void ProcessOrder_OrderNotFound_ThrowsArgumentException()
        {
            _orderRepositoryMock.Setup(r => r.GetById(10)).Returns((Order?)null);

            Assert.Throws<ArgumentException>(() => _orderService.ProcessOrder(10));
        }

        [Fact]
        public void ProcessOrder_AlreadyProcessed_ThrowsInvalidOperationException()
        {
            var order = new Order
            {
                Id = 3,
                CustomerEmail = "done@example.com",
                TotalAmount = 150,
                IsProcessed = true
            };

            _orderRepositoryMock.Setup(r => r.GetById(3)).Returns(order);

            Assert.Throws<InvalidOperationException>(() => _orderService.ProcessOrder(3));
        }

        [Fact]
        public void ProcessOrder_ZeroAmount_ThrowsInvalidOperationException()
        {
            var order = new Order
            {
                Id = 4,
                CustomerEmail = "zero@example.com",
                TotalAmount = 0,
                IsProcessed = false
            };

            _orderRepositoryMock.Setup(r => r.GetById(4)).Returns(order);

            Assert.Throws<InvalidOperationException>(() => _orderService.ProcessOrder(4));
        }

        [Fact]
        public void ProcessOrder_EmptyEmail_DoesNotSendEmail()
        {
            var order = new Order
            {
                Id = 5,
                CustomerEmail = "",
                TotalAmount = 300,
                IsProcessed = false
            };

            _orderRepositoryMock.Setup(r => r.GetById(5)).Returns(order);

            _orderService.ProcessOrder(5);

            _emailServiceMock.Verify(
                e => e.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Never
            );
        }

        [Fact]
        public void DeleteOrder_ExistingOrder_CallsDelete()
        {
            var order = new Order
            {
                Id = 6,
                CustomerEmail = "delete@example.com",
                TotalAmount = 120,
                IsProcessed = false
            };

            _orderRepositoryMock.Setup(r => r.GetById(6)).Returns(order);

            _orderService.DeleteOrder(6);

            _orderRepositoryMock.Verify(r => r.Delete(6), Times.Once);
        }

        [Fact]
        public void DeleteOrder_OrderNotFound_ThrowsArgumentException()
        {
            _orderRepositoryMock.Setup(r => r.GetById(7)).Returns((Order?)null);

            Assert.Throws<ArgumentException>(() => _orderService.DeleteOrder(7));
        }

        [Fact]
        public void GetOrder_ExistingOrder_ReturnsOrder()
        {
            var order = new Order
            {
                Id = 8,
                CustomerEmail = "get@example.com",
                TotalAmount = 500,
                IsProcessed = false
            };

            _orderRepositoryMock.Setup(r => r.GetById(8)).Returns(order);

            var result = _orderService.GetOrder(8);

            Assert.NotNull(result);
            Assert.Equal(8, result!.Id);
            Assert.Equal("get@example.com", result.CustomerEmail);
        }
    }
}