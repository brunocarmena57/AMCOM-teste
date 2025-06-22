namespace Questao5.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public ErrorResponse Error { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        private Result(ErrorResponse error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string message, string type) => new Result<T>(new ErrorResponse(message, type));
    }
}