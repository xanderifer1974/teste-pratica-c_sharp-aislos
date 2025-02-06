using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories;

public class MovimentacaoRepository : IMovimentacaoRepository
{
     private readonly string _connectionString;

     public MovimentacaoRepository(DatabaseConfig databaseConfig)
     {
          _connectionString = databaseConfig.Name;
     }
     public async Task<Movimento> MovimentarContaCorrenteAsync(string requisicaoId, string contaCorrenteId, decimal valor, char tipoMovimento)
     {
          using var connection = new SqliteConnection(_connectionString);

          // Verificar se a requisição já foi processada
          var idemPotencia = await connection.QuerySingleOrDefaultAsync<IdemPotencia>(
              "SELECT * FROM idempotencia WHERE chave_idempotencia = @ChaveIdemPotencia",
              new { ChaveIdemPotencia = requisicaoId });

          if (idemPotencia != null)
          {
               // Retornar o resultado armazenado
               return Newtonsoft.Json.JsonConvert.DeserializeObject<Movimento>(idemPotencia.Resultado);
          }

          if (!await ContaCorrenteCadastradaAsync(connection, contaCorrenteId))
          {
               throw new Exception("INVALID_ACCOUNT");
          }

          if (!await ContaCorrenteAtivaAsync(connection, contaCorrenteId))
          {
               throw new Exception("INACTIVE_ACCOUNT");
          }

          if (valor <= 0)
          {
               throw new Exception("INVALID_VALUE");
          }

          if (tipoMovimento != 'C' && tipoMovimento != 'D')
          {
               throw new Exception("INVALID_TYPE");
          }

          var idMovimento = Guid.NewGuid().ToString();
          var dataMovimento = DateTime.UtcNow.ToString("o");

          var movimento = new Movimento
          {
               IdMovimento = idMovimento,
               NumeroConta = contaCorrenteId,
               DataMovimento = dataMovimento,
               TipoMovimentacao = tipoMovimento == 'C' ? TipoMovimentoEnum.Credito : TipoMovimentoEnum.Debito,
               Valor = valor
          };

          await connection.ExecuteAsync(
              "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)",
              new { IdMovimento = idMovimento, IdContaCorrente = contaCorrenteId, DataMovimento = dataMovimento, TipoMovimento = tipoMovimento, Valor = valor });

          // Armazenar a requisição e o resultado na tabela idempotencia
          var resultado = Newtonsoft.Json.JsonConvert.SerializeObject(movimento);
          await connection.ExecuteAsync(
              "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@ChaveIdemPotencia, @Requisicao, @Resultado)",
              new { ChaveIdemPotencia = requisicaoId, Requisicao = requisicaoId, Resultado = resultado });

          return movimento;
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
}
