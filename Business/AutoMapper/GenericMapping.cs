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
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
        }
    }
}
