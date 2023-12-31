using Sqids;
using System.Numerics;

namespace gitViwe.Shared.MediatR.Implementation;

internal class SqidsIdEncoder<T> : ISqidsIdEncoder<T> where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
{
    private readonly SqidsEncoder<T> _sqids = new(new SqidsOptions
    {
        MinLength = 6,
    });

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
