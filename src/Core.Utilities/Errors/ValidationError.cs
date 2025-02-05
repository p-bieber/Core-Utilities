using Bieber.Core.Utilities.Results;

namespace Bieber.Core.Utilities.Errors;

/// <summary>
/// Represents a validation error with a collection of errors.
/// </summary>
public sealed record ValidationError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> class.
    /// </summary>
    /// <param name="errors">The collection of errors.</param>
    public ValidationError(Error[] errors)
        : base(
            ErrorType.Validation,
            "General.Validation")
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets the collection of errors.
    /// </summary>
    public Error[] Errors { get; }

    /// <summary>
    /// Creates a <see cref="ValidationError"/> instance from a collection of <see cref="Result"/> objects.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result"/> objects.</param>
    /// <returns>A <see cref="ValidationError"/> instance.</returns>
    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}
