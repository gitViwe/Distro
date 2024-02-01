namespace gitViwe.Shared.MongoDB;

/// <summary>
/// A mongoDb database repository to interface with the document database
/// </summary>
/// <typeparam name="TMongoDocument">The document database entity type</typeparam>
public interface IMongoDBRepository<TMongoDocument> where TMongoDocument : MongoDocument
{
    /// <summary>
    /// Creates a queryable source of documents.
    /// </summary>
    /// <returns>Creates a queryable source of documents.</returns>
    IQueryable<TMongoDocument> AsQueryable();

    /// <summary>
    /// Deletes a single document
    /// </summary>
    /// <param name="filterExpression">The filter condition.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be cancelled.</param>
    /// <returns>A Task representing the delete count</returns>
    Task<long> DeleteOneAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the first result or null
    /// </summary>
    /// <param name="id">The <seealso cref="ObjectId.ToString"/> value</param>
    /// <param name="cancellationToken">Propagates notification that operations should be cancelled.</param>
    /// <returns>A Task whose result is the single result or null</returns>
    Task<TMongoDocument> FindByIdAsync(string id, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the first result or null
    /// </summary>
    /// <param name="filterExpression">The filter condition.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be cancelled.</param>
    /// <returns>A Task whose result is the single result or null</returns>
    Task<TMongoDocument> FirstOrDefaultAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);

    /// <summary>
    /// Replace a single document or create an new one if it does not exist
    /// </summary>
    /// <param name="document">The document to insert or replace</param>
    /// <param name="cancellationToken">Propagates notification that operations should be cancelled.</param>
    /// <returns>A Task representing the method</returns>
    Task ReplaceOneAsync(TMongoDocument document, CancellationToken cancellationToken);

    /// <summary>
    /// Each time GetEnumerator is called a new cursor is fetched from the cursor source.
    /// </summary>
    /// <param name="filterExpression">The filter condition.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be cancelled.</param>
    /// <returns>An enumeration of <typeparamref name="TMongoDocument"/></returns>
    IEnumerable<TMongoDocument> ToEnumerable(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);

    /// <summary>
    /// Returns a list containing all the documents returned by the cursor.
    /// </summary>
    /// <param name="filterExpression">The filter condition.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be cancelled.</param>
    /// <returns>A Task whose value is the list of documents</returns>
    Task<List<TMongoDocument>> ToListAsync(Expression<Func<TMongoDocument, bool>> filterExpression, CancellationToken cancellationToken);
}