using System.Globalization;

namespace Bieber.Core.Utilities.Tests.Errors;
public class ErrorTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    public ErrorTests()
    {
        _originalCulture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new("en-US");
        CultureInfo.CurrentUICulture = new("en-US");
    }
    #region Dispose
    private bool _disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                CultureInfo.CurrentCulture = _originalCulture;
                CultureInfo.CurrentUICulture = _originalCulture;
            }
            _disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    [Fact]
    public void Error_Should_Load_Message_From_Resource()
    {
        // Arrange
        var error = new Error(ErrorType.Failure, "General.Null");

        // Act
        var message = error.Message;

        // Assert
        Assert.Equal("Null value was provided", message); // Stelle sicher, dass die Ressource korrekt ist
    }

    [Fact]
    public void Error_Should_Use_Custom_Message_When_Provided()
    {
        // Arrange
        var error = new Error(ErrorType.Problem, "Custom.Code", "This is a custom error message");

        // Act
        var message = error.Message;

        // Assert
        Assert.Equal("This is a custom error message", message);
    }

    [Fact]
    public void Error_None_Should_Be_Correct()
    {
        // Arrange
        var error = Error.None;

        // Act & Assert
        Assert.Equal(string.Empty, error.Code);
        Assert.Equal(string.Empty, error.Message);
        Assert.Equal(ErrorType.Failure, error.Type);
    }

    [Fact]
    public void Error_NullValue_Should_Be_Correct()
    {
        // Arrange
        var error = Error.NullValue;

        // Act & Assert
        Assert.Equal("General.Null", error.Code);
        Assert.Equal("Null value was provided", error.Message);
        Assert.Equal(ErrorType.Failure, error.Type);
    }

    [Fact]
    public void Error_Methods_Should_Create_Correct_Instances()
    {
        // Arrange & Act
        var failure = Error.Failure("Failure.Code", "Failure description");
        var validation = Error.Validation("Validation.Code", "Validation description");

        // Assert
        Assert.Equal("Failure.Code", failure.Code);
        Assert.Equal("Failure description", failure.Message);
        Assert.Equal(ErrorType.Failure, failure.Type);

        Assert.Equal("Validation.Code", validation.Code);
        Assert.Equal("Validation description", validation.Message);
        Assert.Equal(ErrorType.Validation, validation.Type);
    }

    [Fact]
    public void Error_Message_ShouldReturnCustomMessage_WhenCustomMessageIsProvided()
    {
        // Arrange
        var errorType = ErrorType.Validation;
        var code = "CustomCode";
        var customMessage = "Custom error message";
        var error = new Error(errorType, code, customMessage);

        // Act
        var message = error.Message;

        // Assert
        Assert.Equal(customMessage, message);
    }

    [Fact]
    public void Error_Message_ShouldReturnFormattedMessage_WhenFormatArgsAreProvided()
    {
        // Arrange
        var errorType = ErrorType.Validation;
        var code = "FormattedCode";
        var errorMessage = "Message with {0} and {1}";
        var formatArgs = new string[] { "arg1", "arg2" };
        var error = new Error(errorType, code, errorMessage, formatArgs);

        // Act
        var message = error.Message;

        // Assert
        var expectedMessage = "Message with arg1 and arg2";
        Assert.Equal(expectedMessage, message);
    }

    [Fact]
    public void Error_Message_ShouldReturnUnformattedMessage_WhenFormatExceptionThrows()
    {
        // Arrange
        var errorType = ErrorType.Validation;
        var code = "FormattedCode";
        var errorMessage = "Message with {} and {1}";
        var formatArgs = new string[] { "arg1", "arg2" };
        var error = new Error(errorType, code, errorMessage, formatArgs);

        // Act
        var message = error.Message;

        // Assert
        Assert.Equal(errorMessage, message);
    }
}
