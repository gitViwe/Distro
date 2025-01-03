using System.Diagnostics.CodeAnalysis;

namespace gitViwe.Shared.Sqids.Implementation;

internal sealed class DefaultSqidsIdEncoder<T>(IOptions<SqidsIdEncoderOption> options) : ISqidsIdEncoder<T>
    where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
{
    private readonly SqidsEncoder<T> _sqids = new(new SqidsOptions
    {
        MinLength = options.Value.MinLength,
        Alphabet = options.Value.Alphabet,
    });

    public string Encode(T number)
    {
        return _sqids.Encode(number);
    }

    public string Encode(params T[] numbers)
    {
        return _sqids.Encode(numbers);
    }

    public string Encode(IEnumerable<T> numbers)
    {
        return _sqids.Encode(numbers);
    }

    public bool TryDecode(ReadOnlySpan<char> id, out IReadOnlyList<T> decoded)
    {
        decoded = _sqids.Decode(id);
        // Ensuring an ID is "canonical"
        return id.SequenceEqual(_sqids.Encode(decoded));
    }

    public bool TryDecode(ReadOnlySpan<char> id, out T decoded)
    {
        decoded = default;

        // Ensuring output has single element
        if (_sqids.Decode(id) is [var output])
        {
            decoded = output;

            // Ensuring an ID is "canonical"
            return id.SequenceEqual(_sqids.Encode(output));
        }

        return false;
    }
}
