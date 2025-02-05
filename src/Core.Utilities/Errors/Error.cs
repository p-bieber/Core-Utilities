using System.Resources;
namespace Bieber.Core.Utilities.Errors;

/// <summary>
/// Represents an error with a code and type.
/// The message is dynamically retrieved from the resource manager based on the code and current culture.
/// </summary>
public record Error : IEquatable<Error>
{
    static Error()
    {
        ErrorResourceManager.RegisterResourceManager(new ResourceManager(typeof(Errors).FullName!, typeof(ErrorResourceManager).Assembly));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class using the specified code and error type.
    /// The message is retrieved from the resource manager based on the code.
    /// </summary>
    /// <param name="errorType">The type of the error.</param>
    /// <param name="code">The code of the error.</param>
    public Error(ErrorType errorType, string code)
    {
        Code = code;
        Type = errorType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class using the specified code, message, and error type.
    /// </summary>
    /// <param name="errorType">The type of the error.</param>
    /// <param name="code">The code of the error.</param>
    /// <param name="message">The custom message of the error.</param>
    public Error(ErrorType errorType, string code, string? message = null)
        : this(errorType, code)
    {
        _customMessage = message;
    }

    /// <summary>
    /// Gets the code of the error.
    /// </summary>
    public string Code { get; init; }

    /// <summary>
    /// Gets the custom message of the error.
    /// If a custom message is not set, the message is dynamically retrieved from the resource manager based on the code and current culture.
    /// </summary>
    public string Message => _customMessage ?? ErrorResourceManager.GetString(Code);

    private readonly string? _customMessage;

    /// <summary>
    /// Gets the type of the error.
    /// </summary>
    public ErrorType Type { get; init; }

    /// <summary>
    /// Represents no error.
    /// </summary>
    public static readonly Error None = new(ErrorType.Failure, string.Empty, string.Empty);

    /// <summary>
    /// Represents an error for a null value.
    /// </summary>
    public static readonly Error NullValue = new(
        ErrorType.Failure,
        "General.Null");

    /// <summary>
    /// Creates an error of type Failure.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Failure(string code, string? description = default) =>
        new(ErrorType.Failure, code, description);

    /// <summary>
    /// Creates an error of type Validation.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Validation(string code, string? description = default) =>
        new(ErrorType.Validation, code, description);

    /// <summary>
    /// Creates an error of type Problem.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Problem(string code, string? description = default) =>
        new(ErrorType.Problem, code, description);

    /// <summary>
    /// Creates an error of type BadRequest.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error BadRequest(string code, string? description = default) =>
        new(ErrorType.BadRequest, code, description);

    /// <summary>
    /// Creates an error of type NotFound.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error NotFound(string code, string? description = default) =>
        new(ErrorType.NotFound, code, description);

    /// <summary>
    /// Creates an error of type Forbidden.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Forbidden(string code, string? description = default) =>
        new(ErrorType.Forbidden, code, description);

    /// <summary>
    /// Creates an error of type Conflict.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Conflict(string code, string? description = default) =>
        new(ErrorType.Conflict, code, description);

    /// <summary>
    /// Creates an error of type Authentication.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Authentication(string code, string? description = default) =>
        new(ErrorType.Authentication, code, description);

    /// <summary>
    /// Creates an error of type Authorization.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Authorization(string code, string? description = default) =>
        new(ErrorType.Authorization, code, description);

    /// <summary>
    /// Creates an error of type Timeout.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error Timeout(string code, string? description = default) =>
        new(ErrorType.Timeout, code, description);

    /// <summary>
    /// Creates an error of type ServiceUnavailable.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">The description of the error.</param>
    /// <returns>A new instance of the <see cref="Error"/> class.</returns>
    public static Error ServiceUnavailable(string code, string? description = default) =>
        new(ErrorType.ServiceUnavailable, code, description);
}
