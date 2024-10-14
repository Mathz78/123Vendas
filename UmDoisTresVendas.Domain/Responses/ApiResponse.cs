namespace UmDoisTresVendas.Domain.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }

    public ApiResponse(T data)
    {
        Success = true;
        Errors = null;
        Data = data;
    }
    
    public ApiResponse(List<string> errors)
    {
        Success = false;
        Errors = errors;
        Data = default;
    }
}
