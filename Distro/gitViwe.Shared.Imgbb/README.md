## gitViwe.Shared.Imgbb

### Nuget package:
```
dotnet add package gitViwe.Shared.Imgbb 
```

### Image hosting service using Imgbb:
#### Register the `IImgBBClient` service
```
builder.Services.AddGitViweImgBBClient();
```
#### Add configuration options to the `appsettings.json` file
```
{
  "ImgBBClientOption": {
    "APIKey": "my-secret-key",
    "ExpirationInSeconds": 180
  }
}
```

#### Or register the `IImgBBClient` mock service
```
builder.Services.AddGitViweImgBBClientMock();
```

### Usage:

```csharp
IImgBBClient {
    Task<IResponse> PingAsync(CancellationToken cancellation = default);
    Task<ImgBBUploadResponse> UploadImageAsync(IFormFile file, int? expirationInSeconds = null, CancellationToken cancellation = default);
    Task<ImgBBUploadResponse> UploadImageAsync(HttpContent httpContent, string fileName, int? expirationInSeconds = null, CancellationToken cancellation = default);
}
```