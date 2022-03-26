namespace Domain.Results;

public class GetUserResult : BaseResult
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}