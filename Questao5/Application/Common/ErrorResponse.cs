namespace Questao5.Application.Common
{
    public class ErrorResponse
    {
        public string Mensagem { get; }
        public string Tipo { get; }

        public ErrorResponse(string mensagem, string tipo)
        {
            Mensagem = mensagem;
            Tipo = tipo;
        }
    }
}