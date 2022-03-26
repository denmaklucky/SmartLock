namespace Domain.Options;

public class TokenOptions
{
    public const string SectionName = "TokenOptions";
    
    public string ClientSecret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public long TokenLifetime { get; set; }
}