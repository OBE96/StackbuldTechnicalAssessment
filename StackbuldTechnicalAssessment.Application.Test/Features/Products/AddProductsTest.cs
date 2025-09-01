using AutoMapper;
using Moq;
using StackbuldTechnicalAssessment.Application.Features.Products.Commands;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Products.Handlers;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;

namespace StackbuldTechnicalAssessment.Application.Test.Features.Products
{
    public class AddProductCommandHandlerShould
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly AddProductCommandHandler _handler;

        public AddProductCommandHandlerShould()
        {
            // AutoMapper setup
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductCreationDto, Product>();
                cfg.CreateMap<Product, ProductDto>();
            });

            _mapper = config.CreateMapper();
            _mockProductRepository = new Mock<IRepository<Product>>();

            _handler = new AddProductCommandHandler(
                _mockProductRepository.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateProductSuccessfully()
        {
            // Arrange
            var productBody = new ProductCreationDto
            {
                Name = "Test Product",
                Price = 100.0m,
                Description = "Test Description",
                StockQuantity = 10
            };

            _mockProductRepository.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product p) => p);

            _mockProductRepository.Setup(r => r.SaveChanges())
                .Returns(Task.CompletedTask);

            var command = new AddProductsCommand(new List<ProductCreationDto> { productBody });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Products);

            var createdProduct = result.Products[0];
            Assert.Equal(productBody.Name, createdProduct.Name);
            Assert.Equal(productBody.Description, createdProduct.Description);
            Assert.Equal(productBody.Price, createdProduct.Price);
            Assert.Equal(productBody.StockQuantity, createdProduct.StockQuantity);
        }
    }
}
