namespace TrainMaster.Application.Common;

public class ServiceResult
{
    public bool IsSuccess { get; protected set; }
    public string? Error { get; protected set; }
    public int StatusCode { get; protected set; }

    public static ServiceResult Success(int statusCode = 200) =>
        new() { IsSuccess = true, StatusCode = statusCode };

    public static ServiceResult Failure(string error, int statusCode = 400) =>
        new() { IsSuccess = false, Error = error, StatusCode = statusCode };

    public static ServiceResult NotFound(string error = "Resource not found") =>
        Failure(error, 404);
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; private set; }

    public static ServiceResult<T> Success(T data, int statusCode = 200) =>
        new() { IsSuccess = true, Data = data, StatusCode = statusCode };

    public static new ServiceResult<T> Failure(string error, int statusCode = 400) =>
        new() { IsSuccess = false, Error = error, StatusCode = statusCode };

    public static new ServiceResult<T> NotFound(string error = "Resource not found") =>
        Failure(error, 404);
}
