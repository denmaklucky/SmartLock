namespace WebApi.Responses;

public class ErrorResponse
{
    public ErrorResponse(string type, string error) 
        : this(type, new []{error}) {}

    public ErrorResponse(string type, string[] errors)
        => (Type, Errors) = (type, errors);
        
    public string Type { get; set; }
    public string[] Errors { get; set; }
}