namespace gitViwe.Shared.Sqids;

internal class DefaultSqidsIdEncoder<T> : ISqidsIdEncoder<T> where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
{
    private readonly SqidsEncoder<T> _sqids;

    public DefaultSqidsIdEncoder(IOptions<SqidsIdEncoderOption> options)
    {
        _sqids = new(new SqidsOptions
        {
            MinLength = options.Value.MinLength,
            Alphabet = options.Value.Alphabet,
        });
    }

    public IReadOnlyList<T> Decode(ReadOnlySpan<char> id)
    {
        return _sqids.Decode(id);
    }

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
            return id.SequenceEqual(_sqids.Encode(decoded));
        }

        return false;
    }
}
