namespace UmDoisTresVendas.Domain.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
    public T? Content { get; set; }

    public ApiResponse(bool success, List<string> errors)
    {
        Success = false;
        Errors = errors;
        Content = default;
    }
    
    public ApiResponse(T content)
    {
        Success = true;
        Errors = null;
        Content = content;
    }
}
