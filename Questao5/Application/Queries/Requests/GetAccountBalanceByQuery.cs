using MediatR;
using Questao5.Application.Queries.Responses;
using Questao5.Application.Common;

namespace Questao5.Application.Queries.Requests
{
    public class GetAccountBalanceQuery : IRequest<Result<AccountBalanceResponse>>
    {
        public string IdContaCorrente { get; set; }
    }
}