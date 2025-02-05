using System.Globalization;

namespace Bieber.Core.Utilities.Tests.Errors;

public class ValidationErrorTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    public ValidationErrorTests()
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
    public void ValidationError_IsInitializedCorrectly()
    {
        var errors = new[]
        {
                Error.Failure("Code1", "Message1"),
                Error.Failure("Code2", "Message2")
            };

        var validationError = new ValidationError(errors);

        Assert.Equal("General.Validation", validationError.Code);
        Assert.Equal("One or more validation errors occurred", validationError.Message);
        Assert.Equal(ErrorType.Validation, validationError.Type);
        Assert.Equal(errors, validationError.Errors);
    }

    [Fact]
    public void FromResults_CreatesValidationErrorWithCorrectErrors()
    {
        var results = new[]
        {
                Result.Failure(Error.Failure("Code1", "Message1")),
                Result.Success(),
                Result.Failure(Error.Failure("Code2", "Message2"))
            };

        var validationError = ValidationError.FromResults(results);

        var expectedErrors = new[]
        {
                Error.Failure("Code1", "Message1"),
                Error.Failure("Code2", "Message2")
            };

        Assert.Equal(expectedErrors, validationError.Errors);
    }
}

