using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using StackbuldTechnicalAssessment.Application.Features.Orders.Commands;
using StackbuldTechnicalAssessment.Application.Features.Orders.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Orders.Handlers;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;
using StackbuldTechnicalAssessment.Infrastructure.Services.Interfaces;
using Xunit;

namespace StackbuldTechnicalAssessment.Application.Tests.Features.Orders
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IRepository<Order>> _orderRepoMock;
        private readonly Mock<IRepository<OrderDetails>> _orderDetailsRepoMock;
        private readonly Mock<IRepository<Product>> _productRepoMock;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepoMock = new Mock<IRepository<Order>>();
            _orderDetailsRepoMock = new Mock<IRepository<OrderDetails>>();
            _productRepoMock = new Mock<IRepository<Product>>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _mapperMock = new Mock<IMapper>();

            var mockDbTransaction = new Mock<IDbContextTransaction>();
            _transactionServiceMock
                .Setup(t => t.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockDbTransaction.Object);

            _transactionServiceMock
                .Setup(t => t.CommitTransactionAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _transactionServiceMock
                .Setup(t => t.RollbackTransactionAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _orderRepoMock
                .Setup(o => o.AddAsync(It.IsAny<Order>()))
                .ReturnsAsync((Order o) => o);

            _orderDetailsRepoMock
                .Setup(d => d.AddRangeAsync(It.IsAny<IEnumerable<OrderDetails>>()))
                .Returns(Task.CompletedTask);

            _productRepoMock
                .Setup(p => p.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            _handler = new CreateOrderCommandHandler(
                _orderRepoMock.Object,
                _orderDetailsRepoMock.Object,
                 _productRepoMock.Object,
                _transactionServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateOrder_WhenStockIsAvailable()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var dto = new OrderCreateDto
            {
                OrderDetails = new List<OrderDetailDto>
                {
                    new OrderDetailDto { ProductId = productId, Quantity = 2, UnitPrice = 50 }
                }
            };

            var command = new CreateOrderCommand(dto);

            var products = new List<Product>
            {
                new Product { Id = productId, Name = "Shirt", StockQuantity = 5, Price = 50 }
            }.AsQueryable();

            _productRepoMock.Setup(p => p.Query()).Returns(products);

            var mappedDetails = new List<OrderDetails>
            {
                new OrderDetails { ProductId = productId, Quantity = 2, UnitPrice = 50 }
            };
            _mapperMock.Setup(m => m.Map<List<OrderDetails>>(dto))
                .Returns(mappedDetails);

            var mappedHeader = new Order { Id = productId, TotalItems = 2, OrderTotal = 100 };
            _mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderCreateDto>()))
                .Returns(mappedHeader);

            _mapperMock.Setup(m => m.Map<OrderResponseDto>(It.IsAny<Order>()))
                .Returns(new OrderResponseDto { Id = mappedHeader.Id, OrderTotal = 100 });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Data.OrderTotal);

            _productRepoMock.Verify(p => p.UpdateAsync(It.IsAny<Product>()), Times.Once);
            _orderRepoMock.Verify(o => o.AddAsync(It.IsAny<Order>()), Times.Once);
            _orderDetailsRepoMock.Verify(d => d.AddRangeAsync(It.IsAny<IEnumerable<OrderDetails>>()), Times.Once);
            _transactionServiceMock.Verify(t => t.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenStockIsInsufficient()
        {
            var productId = Guid.NewGuid();

            var dto = new OrderCreateDto
            {
                OrderDetails = new List<OrderDetailDto>
                {
                    new OrderDetailDto { ProductId = productId, Quantity = 10, UnitPrice = 50 }
                }
            };
            var command = new CreateOrderCommand(dto);

            var products = new List<Product>
            {
                new Product { Id = productId, Name = "Shirt", StockQuantity = 5, Price = 50 }
            }.AsQueryable();

            _productRepoMock.Setup(p => p.Query()).Returns(products);
            _mapperMock.Setup(m => m.Map<List<OrderDetails>>(dto))
                .Returns(new List<OrderDetails>
                {
                    new OrderDetails { ProductId = productId, Quantity = 10, UnitPrice = 50 }
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
            Assert.Contains("Insufficient stock", result.Message);

            _transactionServiceMock.Verify(t => t.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
