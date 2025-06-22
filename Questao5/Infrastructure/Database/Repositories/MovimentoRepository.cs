using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Threading.Tasks;

public class MovimentoRepository : IMovimentoRepository 
{
    private readonly DatabaseConfig _databaseConfig;
    public MovimentoRepository(DatabaseConfig databaseConfig) => _databaseConfig = databaseConfig;

    public async Task AddAsync(Movimento movimento)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        await connection.ExecuteAsync(
            "INSERT INTO movimento (IdMovimento, IdContaCorrente, DataMovimento, TipoMovimento, Valor) " +
            "VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);",
            movimento);
    }

    public async Task<double> GetTotalByTypeAsync(string idContaCorrente, char tipoMovimento)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        return await connection.ExecuteScalarAsync<double>(
            "SELECT SUM(Valor) FROM movimento WHERE IdContaCorrente = @IdContaCorrente AND TipoMovimento = @TipoMovimento",
            new { IdContaCorrente = idContaCorrente, TipoMovimento = tipoMovimento });
    }
}