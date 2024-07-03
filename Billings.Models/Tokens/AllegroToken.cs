namespace Billings.Models.Tokens;

public class AllegroToken
{
    public int Id { get; private set; }
    public string AccessToken { get; private set; } = null!;
    public string RefreshToken { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private AllegroToken(
        string accessToken,
        string refreshToken,
        DateTime expiresAt,
        DateTime createdAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
    }

    public void Refresh(
        string accessToken,
        string refreshToken,
        int expiresIn)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;

        var createdAt = DateTime.UtcNow;

        ExpiresAt = createdAt.AddSeconds(expiresIn);
        CreatedAt = createdAt;
    }

    public static AllegroToken Create(
        string accessToken,
        string refreshToken,
        int expiresIn)
    {
        var createdAt = DateTime.UtcNow;
        var expiresat = createdAt.AddSeconds(expiresIn);

        return new AllegroToken(
            accessToken,
            refreshToken,
            expiresat,
            createdAt);
    }
}