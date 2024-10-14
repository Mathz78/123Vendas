namespace UmDoisTresVendas.Application.DTOs;

public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
    public T? Content { get; set; }

    public ApiResponseDto(bool success, List<string> errors)
    {
        Success = false;
        Errors = errors;
        Content = default;
    }
    
    public ApiResponseDto(T content)
    {
        Success = true;
        Errors = null;
        Content = content;
    }
}
