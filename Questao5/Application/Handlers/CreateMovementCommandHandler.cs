using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Common;
using Questao5.Application.Interfaces;
using Questao5.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Questao5.Application.Handlers
{
    public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand, Result<CreateMovementResponse>>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public CreateMovementCommandHandler(IContaCorrenteRepository c, IMovimentoRepository m, IIdempotenciaRepository i)
        {
            _contaCorrenteRepository = c;
            _movimentoRepository = m;
            _idempotenciaRepository = i;
        }

        public async Task<Result<CreateMovementResponse>> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
        {
            var idempotencia = await _idempotenciaRepository.GetByIdAsync(request.IdRequisicao);
            if (idempotencia != null)
            {
                var savedResponse = JsonConvert.DeserializeObject<CreateMovementResponse>(idempotencia.Resultado);
                return Result<CreateMovementResponse>.Success(savedResponse);
            }

            var conta = await _contaCorrenteRepository.GetByIdAsync(request.IdContaCorrente);
            if (conta == null)
                return Result<CreateMovementResponse>.Failure("Conta corrente não encontrada.", "INVALID_ACCOUNT");

            if (conta.Ativo == 0)
                return Result<CreateMovementResponse>.Failure("Conta corrente inativa.", "INACTIVE_ACCOUNT");

            if (request.Valor <= 0)
                return Result<CreateMovementResponse>.Failure("O valor do movimento deve ser positivo.", "INVALID_VALUE");
            
            var tipo = request.TipoMovimento.ToUpper();
            if (tipo != "C" && tipo != "D")
                return Result<CreateMovementResponse>.Failure("Tipo de movimento inválido. Use 'C' para Crédito ou 'D' para Débito.", "INVALID_TYPE");

            var movimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = request.IdContaCorrente,
                DataMovimento = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                TipoMovimento = tipo[0],
                Valor = request.Valor
            };
            await _movimentoRepository.AddAsync(movimento);

            var response = new CreateMovementResponse { IdMovimento = movimento.IdMovimento };

            await _idempotenciaRepository.AddAsync(new Idempotencia
            {
                ChaveIdempotencia = request.IdRequisicao,
                Requisicao = JsonConvert.SerializeObject(request),
                Resultado = JsonConvert.SerializeObject(response)
            });

            return Result<CreateMovementResponse>.Success(response);
        }
    }
}