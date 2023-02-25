using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IImportarArquivoBusiness
    {
        Task<bool> IncluirOFX(ImportFileDTO file);
    }
}
