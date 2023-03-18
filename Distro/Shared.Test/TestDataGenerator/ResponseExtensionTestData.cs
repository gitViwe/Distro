using gitViwe.ProblemDetail.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Shared.Test.TestDataGenerator;

internal class ResponseExtensionTestProblemDetailData : IEnumerable<object[]>
{

    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            new Uri("http://localhost:5161/api/account/login"),
            HttpStatusCode.Unauthorized,
            new DefaultProblemDetails("00-573764aa72489e905387dacd9d02ec3f-128aa3b804c069e8-00", new ProblemDetails()
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{StatusCodes.Status401Unauthorized}",
                Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status401Unauthorized),
            }),
            HttpMethod.Post,
            new { Email = "example@email.com", Password = "Password!" },
        },
        new object[]
        {
            new Uri("http://localhost:5161/api/account/login"),
            HttpStatusCode.Forbidden,
            new DefaultProblemDetails("00-573764aa72489e905387dacd9d02ec3f-128aa3b804c069e8-00", new ProblemDetails()
            {
                Status = StatusCodes.Status403Forbidden,
                Type = $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{StatusCodes.Status403Forbidden}",
                Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status403Forbidden),
            }),
            HttpMethod.Post,
            new { Email = "example@email.com", Password = "Password!" },
        },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class ResponseExtensionTestValidationProblemDetailData : IEnumerable<object[]>
{

    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            new Uri("http://localhost:5161/api/account/login"),
            HttpStatusCode.BadRequest,
            new ValidationProblemDetails(new Dictionary<string, string[]>()
            {
                { "property name", new string[]{ "Invalid", "Just plain wrong" } }
            })
            {
                Status = StatusCodes.Status400BadRequest,
                Type = $"https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/{StatusCodes.Status400BadRequest}",
                Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
            },
            HttpMethod.Post,
            new { Email = "email.com", Password = "Password!" },
        },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
