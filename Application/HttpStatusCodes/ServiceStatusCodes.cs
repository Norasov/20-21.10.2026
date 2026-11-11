namespace Application.HttpStatusCodes;

public class ServiceStatusCodes<T>
{

    public StatusCodes Type { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }

    public static ServiceStatusCodes<T> Ok(T data)
    {
        return new ServiceStatusCodes<T>
        {
            Type = StatusCodes.Ok,
            Data = data
        };
    }

    public static ServiceStatusCodes<T> Created(T data) => new ServiceStatusCodes<T> { Type = StatusCodes.Created, Data = data };
    public static ServiceStatusCodes<T> NoContent() => new ServiceStatusCodes<T> { Type = StatusCodes.NoContent };
    public static ServiceStatusCodes<T> NotFound(string error) => new ServiceStatusCodes<T> { Type = StatusCodes.NotFound, Error = error };
    public static ServiceStatusCodes<T> BadRequest(string error) => new ServiceStatusCodes<T> { Type = StatusCodes.BadRequest, Error = error };
    public static ServiceStatusCodes<T> Conflict(string error) => new ServiceStatusCodes<T> { Type = StatusCodes.Conflict, Error = error };
}


