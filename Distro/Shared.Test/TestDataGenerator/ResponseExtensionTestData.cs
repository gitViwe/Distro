using gitViwe.Shared.ProblemDetail.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Shared.Test.TestDataGenerator;

internal class ResponseExtensionTestProblemDetailData : IEnumerable<object[]>
{

    private readonly List<object[]> _data =
    [
        [
            new Uri("http://localhost:5161/api/account/login"),
            HttpStatusCode.Unauthorized,
            new DefaultProblemDetails("00-573764aa72489e905387dacd9d02ec3f-128aa3b804c069e8-00", new DefaultProblemDetails()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Type = $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{(int)HttpStatusCode.Unauthorized}",
                Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Unauthorized),
            }),
            HttpMethod.Post,
            new { Email = "example@email.com", Password = "Password!" },
        ],
        [
            new Uri("http://localhost:5161/api/account/login"),
            HttpStatusCode.Forbidden,
            new DefaultProblemDetails("00-573764aa72489e905387dacd9d02ec3f-128aa3b804c069e8-00", new DefaultProblemDetails()
            {
                Status = (int)HttpStatusCode.Forbidden,
                Type = $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{(int)HttpStatusCode.Forbidden}",
                Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Forbidden),
            }),
            HttpMethod.Post,
            new { Email = "example@email.com", Password = "Password!" },
        ],
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class ResponseExtensionTestValidationProblemDetailData : IEnumerable<object[]>
{

    private readonly List<object[]> _data =
    [
        [
            new Uri("http://localhost:5161/api/account/login"),
            HttpStatusCode.BadRequest,
            new DefaultValidationProblemDetails("00-573764aa72489e905387dacd9d02ec3f-128aa3b804c069e8-00", new DefaultProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{(int)HttpStatusCode.BadRequest}",
                Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            }, new Dictionary<string, string[]>()
            {
                { "property name", new string[]{ "Invalid", "Just plain wrong" } }
            }),
            HttpMethod.Post,
            new { Email = "email.com", Password = "Password!" },
        ],
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
