using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IImportarArquivoRepository
    {
        Task<bool> IncluirOFX(ExtractDTO extract);
    }
}
