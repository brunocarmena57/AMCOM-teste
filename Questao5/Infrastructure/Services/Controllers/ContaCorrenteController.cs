using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Common;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using System.ComponentModel.DataAnnotations;
using Questao5.Application.DTOs;

namespace Questao5.Services.Controllers
{
    [ApiController]
    [Route("api/v1/contacorrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/contacorrente/movimento
        ///     Idempotency-Key: 7C5DFA4A-16C9-ED11-A567-055DFA4A16C9
        ///     {
        ///        "idContaCorrente": "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
        ///        "valor": 100.50,
        ///        "tipoMovimento": "C"
        ///     }
        ///
        /// </remarks>
        [HttpPost("movimento")]
        [ProducesResponseType(typeof(Questao5.Application.Commands.Responses.CreateMovementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMovement(
            [FromHeader(Name = "Idempotency-Key")][Required] string idRequisicao,
            [FromBody] MovementRequestDto body)
        {
            var command = new CreateMovementCommand
            {
                IdRequisicao = idRequisicao,
                IdContaCorrente = body.IdContaCorrente,
                Valor = body.Valor,
                TipoMovimento = body.TipoMovimento
            };

            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{idContaCorrente}/saldo")]
        [ProducesResponseType(typeof(AccountBalanceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBalance([FromRoute] string idContaCorrente)
        {
            var query = new GetAccountBalanceQuery { IdContaCorrente = idContaCorrente };
            var result = await _mediator.Send(query);
            
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}