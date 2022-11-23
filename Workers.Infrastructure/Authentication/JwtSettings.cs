namespace Workers.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Secret { get; set;} = "";
    public string Issuer { get; set;} = "";
    public string Audience { get; set;} = "";
    public int ExpirationTimeInMinutes { get; set;}
}