using AutoMapper;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class ExtratoBusiness : IExtratoBsuiness
    {
        private readonly IExtratoRepository _extratoRepository;
        private readonly IMapper _mapper;

        public ExtratoBusiness(IExtratoRepository extratoRepository, IMapper mapper)
        {
            _extratoRepository = extratoRepository;
            _mapper = mapper;
        }

        public async Task<List<ExtratoViewModel>> ObterExtratoResumido(int usuario)
        {
            try
            {
                var retornoDTO = await _extratoRepository.ObterExtratoResumido(usuario);
                var extratoViewModel = _mapper.Map<List<ExtratoViewModel>>(retornoDTO);
                return extratoViewModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
