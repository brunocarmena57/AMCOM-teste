using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Threading.Tasks;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public IdempotenciaRepository(DatabaseConfig databaseConfig) => _databaseConfig = databaseConfig;

    public async Task<Idempotencia> GetByIdAsync(string chave)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        return await connection.QueryFirstOrDefaultAsync<Idempotencia>(
            "SELECT * FROM idempotencia WHERE chave_idempotencia = @Chave", new { Chave = chave });
    }
    
    public async Task AddAsync(Idempotencia idempotencia)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        await connection.ExecuteAsync(
            "INSERT INTO idempotencia (chave_idempotencia, Requisicao, Resultado) VALUES (@ChaveIdempotencia, @Requisicao, @Resultado);",
            idempotencia);
    }
}