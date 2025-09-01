using AutoMapper;
using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Products.Queries;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;


namespace StackbuldTechnicalAssessment.Application.Features.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {

        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return null;
            }

            var product = await _productRepository.GetAsync(request.Id);
            if (product == null)
            {
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
