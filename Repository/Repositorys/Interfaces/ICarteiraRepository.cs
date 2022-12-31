﻿using Odevez.DTO;
using Odevez.Utils.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface ICarteiraRepository
    {
        Task<List<CarteiraDTO>> ObterDescricaoCarteiraPorUsuario(int usuario);
        Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira();
        Task<List<CategoriaDTO>> ObterCategoriaCarteiraPorUsuario(int usuario);
        Task<List<TipoCarteiraDTO>> ObterTipoCarteira();
        Task<bool> Incluir(TipoCarteiraDTO tipoCarteira);
        Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira);
        Task ExcluirCarteira(int usuario, int carteira);
        Task<decimal> ObterValorPorTipo(int tipoCarteira, int usuario);
        Task<decimal> ObterValorPorUsuario(int usuario);
        Task<decimal> ObterValorPorCodigo(int carteira);
    }
}
