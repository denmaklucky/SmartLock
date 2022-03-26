namespace WebApi.Responses;

public class ErrorResponse
{
    public ErrorResponse(string code)
        : this(code, Array.Empty<string>())
    {
    }

    public ErrorResponse(string code, string error)
        : this(code, new[] { error })
    {
    }

    public ErrorResponse(string code, string[] errors)
        => (ErrorCode, Messages) = (code, errors);
        
    public string ErrorCode { get; set; }
    public string[] Messages { get; set; }
}