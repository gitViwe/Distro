using gitViwe.Shared;

namespace Shared.Test.Data;

internal class TokenResponse : ITokenResponse
{
    public string Token { get; init; }
    public string RefreshToken { get; init; }
}
