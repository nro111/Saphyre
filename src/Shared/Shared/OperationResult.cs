namespace Shared
{
    public class OperationResult<T>
    {
        public bool Success { get; }
        public T Value { get; }
        public string Error { get; }
        public OperationStatus Status { get; }

        private OperationResult(bool success, T value, string error, OperationStatus status)
        {
            Success = success;
            Value = value;
            Error = error;
            Status = status;
        }

        public static OperationResult<T> SuccessResult(T value)
            => new OperationResult<T>(true, value, null, OperationStatus.Ok);

        public static OperationResult<T> FailureResult(string error, OperationStatus status)
            => new OperationResult<T>(false, default, error, status);
    }

    public enum OperationStatus
    {
        Ok = 0,
        NotFound = 1,
        ValidationError = 2,
        Unauthorized = 3,
        Conflict = 4,
        InternalError = 5
    }
}
