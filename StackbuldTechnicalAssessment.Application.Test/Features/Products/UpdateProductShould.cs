using AutoMapper;
using Moq;
using StackbuldTechnicalAssessment.Application.Features.Products.Commands;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Products.Handlers;
using StackbuldTechnicalAssessment.Application.Features.Products.Mappers;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;
using Xunit;

namespace Hng.Application.Test.Features.Products
{
    public class UpdateProductShould
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepository<Product>> _mockRepository;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductShould()
        {
            var mappingProfile = new ProductMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            _mapper = new Mapper(configuration);

            _mockRepository = new Mock<IRepository<Product>>();
            _handler = new UpdateProductCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnUpdatedProduct()
        {
            var productId = Guid.NewGuid();
            var updateDto = new UpdateProductDto
            {
                Name = "Updated Product Name",
                Price = 600.0m,
                Description = "Updated Product Description",
                StockQuantity = 7,
            };

            var initialUpdatedAt = DateTime.UtcNow.AddDays(-1);
            var updatedAt = DateTime.UtcNow;
            var existingProduct = new StackbuldTechnicalAssessment.Domain.Entities.Product
            {
                Id = productId,
                Name = "Old Product Name",
                Price = 500.0m,
                Description = "Old Product Description",
                StockQuantity = 5,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = initialUpdatedAt
            };

            var updatedProduct = new StackbuldTechnicalAssessment.Domain.Entities.Product
            {
                Id = productId,
                Name = updateDto.Name,
                Price = updateDto.Price,
                Description = updateDto.Description,
                StockQuantity = updateDto.StockQuantity,
                CreatedAt = existingProduct.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            var updatedProductDto = new ProductDto
            {
                Id = productId,
                Name = updateDto.Name,
                Price = updateDto.Price,
                Description = updateDto.Description,
                StockQuantity = updateDto.StockQuantity,
                CreatedAt = existingProduct.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.GetAsync(productId))
                .ReturnsAsync(existingProduct);

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<StackbuldTechnicalAssessment.Domain.Entities.Product>()))
                .Callback<StackbuldTechnicalAssessment.Domain.Entities.Product>(product =>
                {
                    product.Id = productId;
                    product.Name = updateDto.Name;
                    product.Price = updateDto.Price;
                    product.Description = updateDto.Description;
                    product.StockQuantity = updateDto.StockQuantity;
                    product.UpdatedAt = updatedAt;
                });

            _mockRepository.Setup(r => r.SaveChanges())
                .Returns(Task.CompletedTask);

            var command = new UpdateProductCommand(productId, updateDto);

            var result = await _handler.Handle(command, default);

            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Price, result.Price);
            Assert.Equal(updateDto.Description, result.Description);
            Assert.Equal(updateDto.StockQuantity, result.StockQuantity);
            Assert.Equal(existingProduct.CreatedAt, result.CreatedAt);
            Assert.True(result.UpdatedAt > initialUpdatedAt);
        }
    }
}