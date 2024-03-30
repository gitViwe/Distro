## gitViwe.Shared.Imgbb

### Nuget package:
```
dotnet add package gitViwe.Shared.Imgbb 
```

### Image hosting service using Imgbb:
#### Register the `IImgBBClient` service by specifying the settings values
```
builder.Services.AddGitViweImgBBClient(options =>
{
    options.APIKey = "my-secret-key";
    options.ExpirationInSeconds = 180;
});
```

### Usage:

```csharp
IImgBBClient {
    Task<IResponse> PingAsync(CancellationToken cancellation = default);
    Task<ImgBBUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default);
    Task<ImgBBUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default);
}
```