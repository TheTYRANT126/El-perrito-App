using System.Collections.Generic;

namespace ElPerrito.Domain.Common
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();

        public static OperationResult SuccessResult(string message = "Operación exitosa")
        {
            return new OperationResult { Success = true, Message = message };
        }

        public static OperationResult FailureResult(string message, params string[] errors)
        {
            return new OperationResult
            {
                Success = false,
                Message = message,
                Errors = new List<string>(errors)
            };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> SuccessResult(T data, string message = "Operación exitosa")
        {
            return new OperationResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public new static OperationResult<T> FailureResult(string message, params string[] errors)
        {
            return new OperationResult<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string>(errors)
            };
        }
    }
}
