## gitViwe.Shared.MongoDB

### Nuget package:
```
dotnet add package gitViwe.Shared.MongoDB 
```

### Mongo document database:
#### Register the `IMongoDbRepository` service using by specifying the settings values
```csharp
builder.Services.AddGitViweMongoDbRepository();
```

#### Add configuration options to the `appsettings.json` file
```
{
  "MongoDbRepositoryOption": {
    "DatabaseName": "hub-db",
    "ConnectionString": "mongodb://root:example@localhost:27017"
  }
}
```

### Usage:

```csharp
IMongoDbRepository<TMongoDocument> {
    IQueryable<TMongoDocument> AsQueryable();
    Task<long> DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
    Task<TMongoDocument> FindByIdAsync(string id, CancellationToken cancellationToken);
    Task<TMongoDocument> FirstOrDefaultAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
    Task ReplaceOneAsync(TMongoDocument document, CancellationToken cancellationToken);
    IEnumerable<TMongoDocument> ToEnumerable(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
    Task<List<TMongoDocument>> ToListAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
}
```