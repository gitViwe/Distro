using Shared.Test.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Shared.Test;

public class ResultsWrapperTests
{
    private readonly ITestOutputHelper _output;
    private Hero _expectedHero = Hero.Get();
    private Villian _expectedVillian = Villian.Get();
    private const string _expectedErrorMessage = "An unhandled error has occurred.";
    private const string _expectedSuccessMessage = "Process completed successfully.";
    private IEnumerable<string> _expectedErrorMessages = new List<string>() { "An unhandled error has occurred.", "You are not authorized to access this resource." };
    private IEnumerable<string> _expectedSuccessMessages = new List<string>() { "Process completed successfully.", "You have been granted access this resource." };

    public ResultsWrapperTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Fail_No_Message()
    {
        var result = Response.Fail();

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);
    }

    [Fact]
    public void Fail_With_Message()
    {
        var result = Response.Fail(_expectedErrorMessage);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain a single element.");
        Assert.True(result.Messages.Count() == 1);

        _output.WriteLine($"'Messages' property must contain the error message: {_expectedErrorMessage}");
        Assert.Contains(_expectedErrorMessage, result.Messages.First());
    }

    [Fact]
    public void Fail_With_Messages()
    {
        var result = Response.Fail(_expectedErrorMessages);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain {_expectedErrorMessages.Count()} elements.");
        Assert.True(result.Messages.Count() == _expectedErrorMessages.Count());

        _output.WriteLine($"'Messages' property must contain the expected error messages.");
        Assert.Equal(_expectedErrorMessages, result.Messages);
    }

    [Fact]
    public void Fail_No_Message_Data()
    {
        var result = Response<Hero>.Fail(_expectedHero);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'Data' property must be type of: {_expectedHero.GetType()}.");
        Assert.IsType<Hero>(result.Data);

        _output.WriteLine($"'Data' property must contain expected data.");
        Assert.Equal(_expectedHero, result.Data);
    }

    [Fact]
    public void Fail_With_Message_Data()
    {
        var result = Response<Hero>.Fail(_expectedErrorMessage, _expectedHero);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain a single element.");
        Assert.True(result.Messages.Count() == 1);

        _output.WriteLine($"'Messages' property must contain the error message: {_expectedErrorMessage}");
        Assert.Contains(_expectedErrorMessage, result.Messages.First());

        _output.WriteLine($"'Data' property must be type of: {_expectedHero.GetType()}.");
        Assert.IsType<Hero>(result.Data);

        _output.WriteLine($"'Data' property must contain expected data.");
        Assert.Equal(_expectedHero, result.Data);
    }

    [Fact]
    public void Fail_With_Messages_Data()
    {
        var result = Response<Hero>.Fail(_expectedErrorMessages, _expectedHero);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have false value.");
        Assert.False(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain {_expectedErrorMessages.Count()} elements.");
        Assert.True(result.Messages.Count() == _expectedErrorMessages.Count());

        _output.WriteLine($"'Messages' property must contain the expected error messages.");
        Assert.Equal(_expectedErrorMessages, result.Messages);

        _output.WriteLine($"'Data' property must be type of: {_expectedHero.GetType()}.");
        Assert.IsType<Hero>(result.Data);

        _output.WriteLine($"'Data' property must contain expected data.");
        Assert.Equal(_expectedHero, result.Data);
    }

    [Fact]
    public void Success_No_Message()
    {
        var result = Response.Success();

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have true value.");
        Assert.True(result.Succeeded);
    }

    [Fact]
    public void Success_With_Message()
    {
        var result = Response.Success(_expectedSuccessMessage);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have true value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain a single element.");
        Assert.True(result.Messages.Count() == 1);

        _output.WriteLine($"'Messages' property must contain the message: {_expectedSuccessMessage}");
        Assert.Contains(_expectedSuccessMessage, result.Messages.First());
    }

    [Fact]
    public void Success_With_Messages()
    {
        var result = Response.Success(_expectedSuccessMessages);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have true value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain {_expectedSuccessMessages.Count()} elements.");
        Assert.True(result.Messages.Count() == _expectedSuccessMessages.Count());

        _output.WriteLine($"'Messages' property must contain the expected messages.");
        Assert.Equal(_expectedSuccessMessages, result.Messages);
    }

    [Fact]
    public void Success_No_Message_Data()
    {
        var result = Response<Villian>.Success(_expectedVillian);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have true value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'Data' property must be type of: {_expectedVillian.GetType()}.");
        Assert.IsType<Villian>(result.Data);

        _output.WriteLine($"'Data' property must contain expected data.");
        Assert.Equal(_expectedVillian, result.Data);
    }

    [Fact]
    public void Success_With_Message_Data()
    {
        var result = Response<Villian>.Success(_expectedSuccessMessage, _expectedVillian);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have true value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain a single element.");
        Assert.True(result.Messages.Count() == 1);

        _output.WriteLine($"'Messages' property must contain the message: {_expectedSuccessMessage}");
        Assert.Contains(_expectedSuccessMessage, result.Messages.First());

        _output.WriteLine($"'Data' property must be type of: {_expectedVillian.GetType()}.");
        Assert.IsType<Villian>(result.Data);

        _output.WriteLine($"'Data' property must contain expected data.");
        Assert.Equal(_expectedVillian, result.Data);
    }

    [Fact]
    public void Success_With_Messages_Data()
    {
        var result = Response<Villian>.Success(_expectedSuccessMessages, _expectedVillian);

        _output.WriteLine($"'Result' object must not be null.");
        Assert.NotNull(result);

        _output.WriteLine($"'Succeeded' property must have true value.");
        Assert.True(result.Succeeded);

        _output.WriteLine($"'Messages' property must contain {_expectedSuccessMessages.Count()} elements.");
        Assert.True(result.Messages.Count() == _expectedSuccessMessages.Count());

        _output.WriteLine($"'Messages' property must contain the expected messages.");
        Assert.Equal(_expectedSuccessMessages, result.Messages);

        _output.WriteLine($"'Data' property must be type of: {_expectedVillian.GetType()}.");
        Assert.IsType<Villian>(result.Data);

        _output.WriteLine($"'Data' property must contain expected data.");
        Assert.Equal(_expectedVillian, result.Data);
    }
}
