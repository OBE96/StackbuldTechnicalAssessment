using AutoMapper;
using Moq;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Products.Handlers;
using StackbuldTechnicalAssessment.Application.Features.Products.Queries;
using StackbuldTechnicalAssessment.Application.Shared.Dtos;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;
using System.Linq.Expressions;
using Xunit;

namespace StackbuldTechnicalAssessment.Application.Test.Features.Products
{
    public class GetProductsQueryHandlerShould
    {
        private readonly Mock<IRepository<Product>> _mockRepository;
        private readonly IMapper _mapper;
        private readonly List<Product> _products;

        public GetProductsQueryHandlerShould()
        {
            _mockRepository = new Mock<IRepository<Product>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
            });

            _mapper = config.CreateMapper();

            _products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product A", Description = "Desc A", Price = 100, StockQuantity = 5 },
                new Product { Id = Guid.NewGuid(), Name = "Product B", Description = "Desc B", Price = 200, StockQuantity = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Product C", Description = "Desc C", Price = 300, StockQuantity = 15 }
            };
        }

        [Fact]
        public async Task ReturnEmptyPagedList_WhenNoProductsExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());

            var handler = new GetProductsQueryHandler(_mockRepository.Object, _mapper);
            var query = new GetProductsQuery { PageNumber = 1, PageSize = 10 };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);

        }
    }
}
