using System.Text.Json.Serialization;
using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Common;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovementCommand : IRequest<Result<CreateMovementResponse>>
    {
        public string IdRequisicao { get; set; }
        public string IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public string TipoMovimento { get; set; }
    }
}