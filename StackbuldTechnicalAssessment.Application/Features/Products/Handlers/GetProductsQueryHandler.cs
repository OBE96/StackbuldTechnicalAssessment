using AutoMapper;
using MediatR;
using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Application.Features.Products.Queries;
using StackbuldTechnicalAssessment.Application.Shared.Dtos;
using StackbuldTechnicalAssessment.Domain.Entities;
using StackbuldTechnicalAssessment.Infrastructure.Repository.Interface;

namespace StackbuldTechnicalAssessment.Application.Features.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedListDto<ProductDto>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedListDto<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);
            var productsResult = PagedListDto<ProductDto>.ToPagedList(mappedProducts, request.PageNumber, request.PageSize);

            return productsResult;
        }
    }
}
