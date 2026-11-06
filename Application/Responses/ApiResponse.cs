using System.Net;

namespace Application.Response;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode {get;set;}
    public string? Message {get;set;}
    public T? Data {get;set;}

    // Этот конструктор нужен для того чтобы вывести успешный запрос
    public ApiResponse(HttpStatusCode statusCode, string message, T data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    // А этот конструктор нужен для того чтобы показать ошибку запроса
    public ApiResponse(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

}
