using Odevez.API.ViewModel;

namespace Odevez.Business.ViewModel
{
    public class ExtratoViewModel
    {
        public int Codigo { get; set; }
        public string DataMovimentacao { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Usuario { get; set; }
        public CategoriaExtratoViewModel Categoria { get; set; }
        public MovimentacaoExtratoViewModel Movimentacao { get; set; }
        public CarteiraExtratoViewModel Carteira { get; set; }
    }
}
