using System.Numerics;

namespace gitViwe.Shared.Abstraction;

/// <summary>
/// An abstraction on top of Sqids, a small library that generates YouTube-like IDs from numbers.
/// </summary>
/// <typeparam name="T">The input data type.</typeparam>
public interface ISqidsIdEncoder<T> where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Encodes a single number into a Sqids ID.
    /// </summary>
    /// <param name="number">The number to encode.</param>
    /// <returns>A string containing the encoded ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="ArgumentException" />
    string Encode(T number);

    /// <summary>
    /// Encodes multiple numbers into a Sqids ID.
    /// </summary>
    /// <param name="numbers">The numbers to encode.</param>
    /// <returns>A string containing the encoded ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="ArgumentException" />
    string Encode(params T[] numbers);

    /// <summary>
    /// Encodes a collection of numbers into a Sqids ID.
    /// </summary>
    /// <param name="numbers">The numbers to encode.</param>
    /// <returns>A string containing the encoded ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    /// <exception cref="ArgumentException" />
    string Encode(IEnumerable<T> numbers);

    /// <summary>
    /// Decodes an ID into numbers.
    /// </summary>
    /// <param name="id">The encoded ID.</param>
    /// <returns>
    /// An array containing the decoded number(s) (it would contain only one element
	/// if the ID represents a single number); or an empty array if the input ID is null,
	/// empty, or includes characters not found in the alphabet.
    /// </returns>
    /// <remarks>
    /// This method does not ensure an ID is "canonical". Consider using <seealso cref="TryDecode(ReadOnlySpan{char}, out IReadOnlyList{T})"/>
    /// <br /><see href="https://github.com/sqids/sqids-dotnet/tree/main#ensuring-an-id-is-canonical"/>
    /// </remarks>
    [Obsolete(message: "This method does not ensure an ID is canonical.", error: false)]
    IReadOnlyList<T> Decode(ReadOnlySpan<char> id);

    /// <summary>
    /// Attempts to decode an ID into numbers and ensures an ID is "canonical".
    /// <br /><see href="https://github.com/sqids/sqids-dotnet/tree/main#ensuring-an-id-is-canonical"/>
    /// </summary>
    /// <param name="id">The encoded ID.</param>
    /// <param name="decoded">
    /// An array containing the decoded number(s) (it would contain only one element
	/// if the ID represents a single number); or an empty array if the input ID is null,
	/// empty, or includes characters not found in the alphabet.
    /// </param>
    /// <returns>True if the decode was successful.</returns>
    bool TryDecode(ReadOnlySpan<char> id, out IReadOnlyList<T> decoded);

    /// <summary>
    /// Attempts to decode an ID into numbers and ensures an ID is "canonical".
    /// <br /><see href="https://github.com/sqids/sqids-dotnet/tree/main#ensuring-an-id-is-canonical"/>
    /// </summary>
    /// <param name="id">The encoded ID.</param>
    /// <param name="decoded">
    /// The decoded number or default value of <typeparamref name="T"/> if the input ID is null,
	/// empty, or includes characters not found in the alphabet.
    /// </param>
    /// <returns></returns>
    bool TryDecode(ReadOnlySpan<char> id, out T decoded);
}
