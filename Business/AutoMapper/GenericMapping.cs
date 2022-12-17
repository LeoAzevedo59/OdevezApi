using AutoMapper;
using Odevez.API.ViewModel;
using Odevez.Business.ViewModel;
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
            CreateMap<TipoCarteiraDTO, TipoCarteiraViewModel>().ReverseMap();

            CreateMap<ExtratoDTO, ExtratoViewModel>().ReverseMap();
            CreateMap<ExtratoMesFiltroDTO, ExtratoMesFiltroViewModel>().ReverseMap();
            CreateMap<CategoriaDTO, CategoriaExtratoViewModel>().ReverseMap();
            CreateMap<MovimentacaoDTO, MovimentacaoExtratoViewModel>().ReverseMap();
            CreateMap<CarteiraDTO, CarteiraExtratoViewModel>().ReverseMap();
        }
    }
}
