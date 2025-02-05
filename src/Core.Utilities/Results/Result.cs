using Bieber.Core.Utilities.Errors;

namespace Bieber.Core.Utilities.Results;

/// <summary>
/// Represents the result of an operation, with a success status and an associated error.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The associated error.</param>
    /// <exception cref="ArgumentException">Thrown when the error is invalid for the success status.</exception>
    protected internal Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) ||
            (!isSuccess && error == Error.None))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the associated error.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful result.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A successful result with a value.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    /// <summary>
    /// Creates a failed result with an error.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>A failed result with an error.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Creates a failed result with an error and a default value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error.</param>
    /// <returns>A failed result with an error and a default value.</returns>
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    /// <summary>
    /// Creates a result based on the provided value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A successful result if the value is not null; otherwise, a failed result.</returns>
    public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);


    /// <summary>
    /// Implicitly converts an error to a failed result.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(Error error) => Failure(error);
}
