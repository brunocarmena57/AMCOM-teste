using Questao5.Domain.Entities;
using System.Threading.Tasks;

namespace Questao5.Application.Interfaces
{
    public interface IIdempotenciaRepository
    {
        Task<Idempotencia> GetByIdAsync(string chave);
        Task AddAsync(Idempotencia idempotencia);
    }
}