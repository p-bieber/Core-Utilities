using System.Globalization;

namespace Bieber.Core.Utilities.Tests.Errors;

public class ErrorResourceManagerTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    public ErrorResourceManagerTests()
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
    public void GetString_Should_Return_Message_From_Resource()
    {
        // Arrange


        // Act
        var message = ErrorResourceManager.GetString("General.Null");

        // Assert
        Assert.Equal("Null value was provided", message);
    }

    [Fact]
    public void GetString_Should_Return_Default_Message_When_Key_Not_Found()
    {
        // Act
        var message = ErrorResourceManager.GetString("Nonexistent.Key");

        // Assert
        Assert.Equal("Missing localization for key: Nonexistent.Key", message);
    }
}
