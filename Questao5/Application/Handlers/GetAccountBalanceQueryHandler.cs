using MediatR;
using Questao5.Application.Common;
using Questao5.Application.Interfaces;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Questao5.Application.Handlers
{
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, Result<AccountBalanceResponse>>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public GetAccountBalanceQueryHandler(IContaCorrenteRepository c, IMovimentoRepository m)
        {
            _contaCorrenteRepository = c;
            _movimentoRepository = m;
        }

        public async Task<Result<AccountBalanceResponse>> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);
            if (conta == null)
                return Result<AccountBalanceResponse>.Failure("Conta corrente não encontrada.", "INVALID_ACCOUNT");

            if (conta.Ativo == 0)
                return Result<AccountBalanceResponse>.Failure("Conta corrente inativa.", "INACTIVE_ACCOUNT");
            
            double creditos = await _movimentoRepository.GetTotalByTypeAsync(request.IdContaCorrente, 'C');
            double debitos = await _movimentoRepository.GetTotalByTypeAsync(request.IdContaCorrente, 'D');
            double saldo = creditos - debitos;

            var response = new AccountBalanceResponse
            {
                NumeroContaCorrente = conta.Numero,
                NomeTitular = conta.Nome,
                DataHoraConsulta = DateTime.Now,
                SaldoAtual = saldo
            };

            return Result<AccountBalanceResponse>.Success(response);
        }
    }
}