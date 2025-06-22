using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Threading.Tasks;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ContaCorrenteRepository(DatabaseConfig databaseConfig) => _databaseConfig = databaseConfig;

    public async Task<ContaCorrente> GetByIdAsync(string id)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(
            "SELECT * FROM contacorrente WHERE idcontacorrente = @Id", new { Id = id });
    }
}