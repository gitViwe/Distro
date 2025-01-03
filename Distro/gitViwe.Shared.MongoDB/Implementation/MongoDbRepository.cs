﻿namespace gitViwe.Shared.MongoDB.Implementation;

internal sealed class MongoDbRepository<TMongoDocument>(IOptions<MongoDbRepositoryOption> options) : IMongoDbRepository<TMongoDocument>
    where TMongoDocument : MongoDocument
{
    private readonly IMongoCollection<TMongoDocument> _collection = GetCollectionName(options.Value);

    private static IMongoCollection<TMongoDocument> GetCollectionName(MongoDbRepositoryOption options)
    {
        var database = new MongoClient(options.MongoClientSettings).GetDatabase(options.DatabaseName);
        
        var data = typeof(TMongoDocument).GetCustomAttribute<BsonCollectionAttribute>()
                   ?? throw new ArgumentException($"'{nameof(TMongoDocument)}' does not have the class attribute/decorator '{nameof(BsonCollectionAttribute)}'");

        return database.GetCollection<TMongoDocument>(data.CollectionName);
    }

    public Task<TMongoDocument> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, objectId);
        return _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<TMongoDocument> FirstOrDefaultAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return _collection.Find(filterExpression).FirstOrDefaultAsync(cancellationToken);
    }

    public IEnumerable<TMongoDocument> ToEnumerable(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return _collection.Find(filterExpression).ToEnumerable(cancellationToken);
    }

    public Task<List<TMongoDocument>> ToListAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken)
    {
        return _collection.Find(filterExpression).ToListAsync(cancellationToken);
    }

    public Task ReplaceOneAsync(TMongoDocument document, CancellationToken cancellationToken)
    {
        var filter = Builders<TMongoDocument>.Filter.Eq(doc => doc.Id, document.Id);
        return _collection.ReplaceOneAsync(filter, document, new ReplaceOptions { IsUpsert = true }, cancellationToken);
    }

    public async Task<long> DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken)
    {
        var result = await _collection.DeleteOneAsync(filterExpression, cancellationToken);
        return result.IsAcknowledged ? result.DeletedCount : 0;
    }

    public IQueryable<TMongoDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }
}