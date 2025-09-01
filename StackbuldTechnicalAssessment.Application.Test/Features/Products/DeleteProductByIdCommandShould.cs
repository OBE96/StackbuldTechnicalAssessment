using AutoMapper;
using Moq;
using System;
using StackbuldTechnicalAssessment.Application.Features.Products.Commands;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Products.Handlers;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;

namespace StackbuldTechnicalAssessment.Application.Test.Features.Products
{
    public class DeleteProductByIdCommandShould
    {
        private readonly Mock<IRepository<Product>> _mockRepository;
        private readonly IMapper _mapper;
        private readonly List<Product> products;

        public DeleteProductByIdCommandShould()
        {
            _mockRepository = new Mock<IRepository<Product>>();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
            });

            _mapper = config.CreateMapper();

            // Test data
            products = new List<Product>
            {
                new Product { Id = new Guid("9BF7A95A-40F5-463F-9CEC-3C492A1810BC"), Name = "Fine Cloths", Description = "a fine cloth", Price = 10.00m, StockQuantity = 2 },
                new Product { Id = Guid.NewGuid(), Name = "60 Electrical bulb", Description = "a bulb", Price = 1.35m , StockQuantity = 5},
                new Product { Id = Guid.NewGuid(), Name = "First Blood", Description = "what a film", Price = 45.56m ,  StockQuantity = 7}
            };
        }

        [Fact]
        public async Task DeleteProduct_WhenProductExists()
        {
            // Arrange
            var productId = new Guid("9BF7A95A-40F5-463F-9CEC-3C492A1810BC");
            var product = products.First(p => p.Id == productId);

            _mockRepository.Setup(repo => repo.GetAsync(productId))
                .ReturnsAsync(product);


            var handler = new DeleteProductByIdCommandHandler(_mockRepository.Object, _mapper);
            var command = new DeleteProductByIdCommand(productId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            _mockRepository.Verify(repo => repo.DeleteAsync(product), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task DoesNotDelete_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mockRepository.Setup(repo => repo.GetAllAsync())
               .ReturnsAsync(products);

            var handler = new DeleteProductByIdCommandHandler(_mockRepository.Object, _mapper);
            var command = new DeleteProductByIdCommand(productId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Product>()), Times.Never);
            _mockRepository.Verify(repo => repo.SaveChanges(), Times.Never);
        }
    }
}
