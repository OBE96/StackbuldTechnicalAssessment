using AutoMapper;
using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Commands;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;

namespace StackbuldTechnicalAssessment.Application.Features.Products.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductsCommand, ProductsDto>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductsDto> Handle(AddProductsCommand request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<List<Product>>(request.productBody);

            foreach (var product in products)
            {
                await _productRepository.AddAsync(product);
            }

            await _productRepository.SaveChanges();

            return new ProductsDto
            {
                Products = _mapper.Map<List<ProductDto>>(products)
            };

        }
    }
}
