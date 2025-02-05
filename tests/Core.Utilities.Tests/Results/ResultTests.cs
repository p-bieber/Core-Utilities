namespace Bieber.Core.Utilities.Tests.Results;

public class ResultTests
{
    [Fact]
    public void Success_CreatesSuccessfulResult()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Failure_CreatesFailedResult()
    {
        var error = Error.Failure("Error.Code", "Error message");
        var result = Result.Failure(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Create_WithNonNullValue_CreatesSuccessfulResult()
    {
        var value = "test";
        var result = Result.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Create_WithNullValue_CreatesFailedResult()
    {
        var result = Result.Create<string>(null);

        Assert.False(result.IsSuccess);
        Assert.Equal(Error.NullValue, result.Error);
    }

    [Fact]
    public void Value_AccessedOnFailedResult_ThrowsException()
    {
        var result = Result.Failure<string>(Error.Failure("Error.Code", "Error message"));

        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void ImplicitConversion_FromValue_CreatesSuccessfulResult()
    {
        string value = "test";
        Result<string> result = value;

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void ImplicitConversion_FromError_CreatesFailedResult()
    {
        var error = Error.Failure("Error.Code", "Error message");
        Result<string> result = error;

        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }
}

