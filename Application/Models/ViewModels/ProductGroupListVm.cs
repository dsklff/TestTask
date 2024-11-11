using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Models.ViewModels
{
    public class ProductGroupListVm : IMapFrom<ProductGroup>
    {
        public required string Name { get; set; }
        public double TotalPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductGroup, ProductGroupListVm>()
                .ForMember(dest => dest.TotalPrice, opt =>
                    opt.MapFrom(src => src.ProductGroupItems.Sum(pgi => pgi.ProcessedQuantity * pgi.Product.Price)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => $"Группа {x.Number}"));
        }
    }
}
