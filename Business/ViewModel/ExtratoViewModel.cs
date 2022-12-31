using Odevez.API.ViewModel;
using Odevez.Utils.Enum;

namespace Odevez.Business.ViewModel
{
    public class ExtratoViewModel
    {
        public int Codigo { get; set; }
        public string DataCriacao { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public ExtratoStatusEnum Status { get; set; }
        public CategoriaExtratoViewModel Categoria { get; set; }
        public MovimentacaoExtratoViewModel Movimentacao { get; set; }
        public CarteiraExtratoViewModel Carteira { get; set; }
    }
}
