namespace gitViwe.Shared.Imgbb.Contract;

/// <summary>
/// The response object from performing an upload
/// </summary>
public sealed class ImgBBUploadResponse
{
    /// <summary>
    /// The image data
    /// </summary>
    [JsonPropertyName("data")]
    public ImgBBData Data { get; set; } = new();

    /// <summary>
    /// Indicates if the upload was successful
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Represents the status code
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }
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
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The image title
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The uri
    /// </summary>
    [JsonPropertyName("url_viewer")]
    public string UrlViewer { get; set; } = string.Empty;

    /// <summary>
    /// The uri
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The display uri
    /// </summary>
    [JsonPropertyName("display_url")]
    public string DisplayUrl { get; set; } = string.Empty;

    /// <summary>
    /// THe image dimensions
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// THe image dimensions
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// The image size
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// The time image was created
    /// </summary>
    [JsonPropertyName("time")]
    public int Time { get; set; }

    /// <summary>
    /// The expiration date in unix
    /// </summary>
    [JsonPropertyName("expiration")]
    public int Expiration { get; set; }

    /// <summary>
    /// The main image
    /// </summary>
    [JsonPropertyName("image")]
    public ImgBBImage Image { get; set; } = new();

    /// <summary>
    /// The image thumbnail
    /// </summary>
    [JsonPropertyName("thumb")]
    public ImgBBThumb Thumb { get; set; } = new();

    /// <summary>
    /// The link to delete the image
    /// </summary>
    [JsonPropertyName("delete_url")]
    public string DeleteUrl { get; set; } = string.Empty;
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
    public string Filename { get; set; } = string.Empty;

    /// <summary>
    /// The image display name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// THe mime type
    /// </summary>
    [JsonPropertyName("mime")]
    public string Mime { get; set; } = string.Empty;

    /// <summary>
    /// Additional information
    /// </summary>
    [JsonPropertyName("extension")]
    public string Extension { get; set; } = string.Empty;

    /// <summary>
    /// The image src uri
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
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
    public string Filename { get; set; } = string.Empty;

    /// <summary>
    /// The image display name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// THe mime type
    /// </summary>
    [JsonPropertyName("mime")]
    public string Mime { get; set; } = string.Empty;

    /// <summary>
    /// Additional information
    /// </summary>
    [JsonPropertyName("extension")]
    public string Extension { get; set; } = string.Empty;

    /// <summary>
    /// The image src uri
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
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
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Error code
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
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
    public int StatusCode { get; set; }

    /// <summary>
    /// Error detail
    /// </summary>
    [JsonPropertyName("error")]
    public ImgBBClientPingError Error { get; set; } = new();

    /// <summary>
    /// The status code description
    /// </summary>
    [JsonPropertyName("status_txt")]
    public string StatusText { get; set; } = string.Empty;
}
