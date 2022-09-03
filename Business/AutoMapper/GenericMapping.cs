using AutoMapper;
using Odevez.DTO;
using Odevez.Repository.Models;

namespace Odevez.Business.GenericMapping
{
    public class GenericMapping : Profile
    {
        public GenericMapping()
        {
            CreateMap<ClientDTO, ClientModel>().ReverseMap();
            CreateMap<OrderDTO, OrderModel>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItemModel>().ReverseMap();
            CreateMap<ProductDTO, ProductModel>().ReverseMap();
            CreateMap<UserDTO, UserModel>().ReverseMap();
        }
    }
}
