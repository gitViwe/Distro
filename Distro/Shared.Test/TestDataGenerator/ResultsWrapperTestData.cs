using Shared.Test.Model;
using System.Collections;
using System.Collections.Generic;

namespace Shared.Test.TestDataGenerator;

internal class PaginatedResponseTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            new TokenResponse[]
            {
                new() { },
                new() { },
                new() { },
                new() { },
                new() { },
            },
            5,
            1,
            3
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
