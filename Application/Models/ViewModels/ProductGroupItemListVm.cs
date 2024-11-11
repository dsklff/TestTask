using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Models.ViewModels
{
    public class ProductGroupItemListVm : IMapFrom<ProductGroupItem>
    {
        public required string GroupName { get; set; }
        public required string Name { get; set; }
        public required string UnitOfMeasure { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }      

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductGroupItem, ProductGroupItemListVm>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(x => $"Группа {x.ProductGroup.Number}"))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Product.Name))
                .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(x => x.Product.UnitOfMeasure))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(x => x.Product.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(x => x.ProcessedQuantity));
        }
    }
}
