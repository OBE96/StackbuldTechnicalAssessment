using StackbuldTechnicalAssessment.Application.Features.Products.Dtos;
using StackbuldTechnicalAssessment.Domain.Entities;


namespace StackbuldTechnicalAssessment.Application.Features.Products.Mappers
{
    public class ProductMapperProfile : AutoMapper.Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCreationDto, Product>();
            CreateMap<UpdateProductDto, Product>();

        }
    }
}
