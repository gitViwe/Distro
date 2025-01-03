namespace gitViwe.Shared.Imgbb;

/// <summary>
/// The response object from performing an upload
/// </summary>
public sealed class ImgBbUploadResponse
{
    /// <summary>
    /// The image data
    /// </summary>
    [JsonPropertyName("data")]
    public ImgBBData Data { get; init; } = new();

    /// <summary>
    /// Indicates if the upload was successful
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// Represents the status code
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; init; }
}

/// <summary>
/// The image data
/// </summary>
public sealed class ImgBBData
{
    /// <summary>
    /// The image id
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The image title
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// The uri
    /// </summary>
    [JsonPropertyName("url_viewer")]
    public string UrlViewer { get; init; } = string.Empty;

    /// <summary>
    /// The uri
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    /// <summary>
    /// The display uri
    /// </summary>
    [JsonPropertyName("display_url")]
    public string DisplayUrl { get; init; } = string.Empty;

    /// <summary>
    /// THe image dimensions
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; init; }

    /// <summary>
    /// THe image dimensions
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; init; }

    /// <summary>
    /// The image size
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; init; }

    /// <summary>
    /// The time image was created
    /// </summary>
    [JsonPropertyName("time")]
    public int Time { get; init; }

    /// <summary>
    /// The expiration date in unix
    /// </summary>
    [JsonPropertyName("expiration")]
    public int Expiration { get; init; }

    /// <summary>
    /// The main image
    /// </summary>
    [JsonPropertyName("image")]
    public ImgBBImage Image { get; init; } = new();

    /// <summary>
    /// The image thumbnail
    /// </summary>
    [JsonPropertyName("thumb")]
    public ImgBBThumb Thumb { get; init; } = new();

    /// <summary>
    /// The link to delete the image
    /// </summary>
    [JsonPropertyName("delete_url")]
    public string DeleteUrl { get; init; } = string.Empty;
}

/// <summary>
/// The main image data
/// </summary>
public sealed class ImgBBImage
{
    /// <summary>
    /// The image file name
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; init; } = string.Empty;

    /// <summary>
    /// The image display name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// THe mime type
    /// </summary>
    [JsonPropertyName("mime")]
    public string Mime { get; init; } = string.Empty;

    /// <summary>
    /// Additional information
    /// </summary>
    [JsonPropertyName("extension")]
    public string Extension { get; init; } = string.Empty;

    /// <summary>
    /// The image src uri
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}

/// <summary>
/// The image thumbnail
/// </summary>
public sealed class ImgBBThumb
{
    /// <summary>
    /// The image file name
    /// </summary>
    [JsonPropertyName("filename")]
    public string Filename { get; init; } = string.Empty;

    /// <summary>
    /// The image display name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// THe mime type
    /// </summary>
    [JsonPropertyName("mime")]
    public string Mime { get; init; } = string.Empty;

    /// <summary>
    /// Additional information
    /// </summary>
    [JsonPropertyName("extension")]
    public string Extension { get; init; } = string.Empty;

    /// <summary>
    /// The image src uri
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}

/// <summary>
/// The error detail
/// </summary>
public sealed class ImgBBClientPingError
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; init; }
}

/// <summary>
/// The ping response
/// </summary>
public sealed class ImgBBClientPingErrorResponse
{
    /// <summary>
    /// Error status code
    /// </summary>
    [JsonPropertyName("status_code")]
    public int StatusCode { get; init; }

    /// <summary>
    /// Error detail
    /// </summary>
    [JsonPropertyName("error")]
    public ImgBBClientPingError Error { get; init; } = new();

    /// <summary>
    /// The status code description
    /// </summary>
    [JsonPropertyName("status_txt")]
    public string StatusText { get; init; } = string.Empty;
}
