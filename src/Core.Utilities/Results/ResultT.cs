using Bieber.Core.Utilities.Errors;
using System.Diagnostics.CodeAnalysis;

namespace Bieber.Core.Utilities.Results;

/// <summary>
/// Represents the result of an operation with a return value, success status, and an associated error.
/// </summary>
/// <typeparam name="TValue">The type of the return value of the operation.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The return value associated with the result.</param>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The associated error.</param>
    /// <exception cref="ArgumentException">Thrown when the error is invalid for the success status.</exception>
    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        _value = value;

    /// <summary>
    /// Gets the return value associated with the result.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when attempting to access the value of a failed result.</exception>
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    /// <summary>
    /// Implicitly converts a return value to a successful result.
    /// </summary>
    /// <param name="value">The return value to convert.</param>
    public static implicit operator Result<TValue>(TValue? value) => Create(value);

    /// <summary>
    /// Implicitly converts an error to a failed result.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result<TValue>(Error error) => Failure<TValue>(error);

    /// <summary>
    /// Creates a validation failure result.
    /// </summary>
    /// <param name="error">The validation error.</param>
    /// <returns>A validation failure result.</returns>
    public static Result<TValue> ValidationFailure(Error error) =>
       new(default, false, error);
}

