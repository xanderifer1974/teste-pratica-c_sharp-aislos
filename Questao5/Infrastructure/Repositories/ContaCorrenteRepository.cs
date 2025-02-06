using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{   
     private readonly string _connectionString;

    public ContaCorrenteRepository(DatabaseConfig databaseConfig)
    {
        _connectionString = databaseConfig.Name;
    }

    public async Task<SaldoResponse> ConsultarSaldoAsync(string contaCorrenteId)
    {
        using var connection = new SqliteConnection(_connectionString);

        if (!await ContaCorrenteCadastradaAsync(connection, contaCorrenteId))
        {
            throw new Exception("INVALID_ACCOUNT");
        }

        if (!await ContaCorrenteAtivaAsync(connection, contaCorrenteId))
        {
            throw new Exception("INACTIVE_ACCOUNT");
        }

        var saldo = await CalcularSaldoAsync(connection, contaCorrenteId);

        var conta = await connection.QuerySingleOrDefaultAsync<ContaCorrente>(
            "SELECT numero, nome FROM contacorrente WHERE idcontacorrente = @Id",
            new { Id = contaCorrenteId });

        return new SaldoResponse
        {
            NumeroContaCorrente = conta.Numero.ToString(),
            NomeTitular = conta.Nome,
            DataHoraResposta = DateTime.Now,
            ValorSaldo = saldo
        };
    }

    private async Task<bool> ContaCorrenteCadastradaAsync(IDbConnection connection, string contaCorrenteId)
    {
        var result = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM contacorrente WHERE idcontacorrente = @Id",
            new { Id = contaCorrenteId });

        return result > 0;
    }

    private async Task<bool> ContaCorrenteAtivaAsync(IDbConnection connection, string contaCorrenteId)
    {
        var result = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM contacorrente WHERE idcontacorrente = @Id AND ativo = 1",
            new { Id = contaCorrenteId });

        return result > 0;
    }

    private async Task<decimal> CalcularSaldoAsync(IDbConnection connection, string contaCorrenteId)
    {
        var creditos = await connection.ExecuteScalarAsync<decimal>(
            "SELECT IFNULL(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'C'",
            new { Id = contaCorrenteId });

        var debitos = await connection.ExecuteScalarAsync<decimal>(
            "SELECT IFNULL(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'D'",
            new { Id = contaCorrenteId });

        return creditos - debitos;
    }
}
