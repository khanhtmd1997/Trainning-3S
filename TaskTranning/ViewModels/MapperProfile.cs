using AutoMapper;
using TaskTranning.Models;

namespace TaskTranning.ViewModels
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Brand, BrandViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, EditPictureProductViewModel>();
            CreateMap<Stock, StockViewModel>();
            CreateMap<Store, StoreViewModel>();
            CreateMap<User, AddUserViewModel>();
            CreateMap<User, EditPasswordUserViewModel>();
            CreateMap<User, EditUserViewModel>();
        }
    }
}