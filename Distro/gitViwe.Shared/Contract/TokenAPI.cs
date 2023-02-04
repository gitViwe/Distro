namespace gitViwe.Shared;

/// <summary>
/// The request object to perform a login
/// </summary>
public interface ILoginRequest
{
    /// <summary>
    /// The user's email address
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// The user's password
    /// </summary>
    string Password { get; set; }
}

/// <summary>
/// The request object to perform a registration
/// </summary>
public interface IRegisterRequest
{
    /// <summary>
    /// The user's username
    /// </summary>
    string UserName { get; set; }

    /// <summary>
    /// The user's email address
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// The user's password
    /// </summary>
    string Password { get; set; }

    /// <summary>
    /// The user's password
    /// </summary>
    string PasswordConfirmation { get; set; }
}

/// <summary>
/// The request object to perform a token refresh
/// </summary>
public interface ITokenRequest
{
    /// <summary>
    /// The user's current token
    /// </summary>
    string Token { get; init; }

    /// <summary>
    /// The user's current refresh token
    /// </summary>
    string RefreshToken { get; init; }
}

/// <summary>
/// The response object for an authenticated user
/// </summary>
public interface ITokenResponse : ITokenRequest { }

/// <summary>
/// A unified paginated request type for the API endpoint
/// </summary>
public interface IPaginatedRequest
{
    /// <summary>
    /// The current page number
    /// </summary>
    int CurrentPage { get; set; }

    /// <summary>
    /// The number of items in a single page
    /// </summary>
    int PageSize { get; set; }
}