using Questao5.Domain.Entities;
using System.Threading.Tasks;

namespace Questao5.Application.Interfaces
{
    public interface IMovimentoRepository
    {
        Task AddAsync(Movimento movimento);
        Task<double> GetTotalByTypeAsync(string idContaCorrente, char tipoMovimento);
    }
}