using Bieber.Core.Utilities.Errors;

namespace Bieber.Core.Utilities.Results;
/// <summary>
/// Provides extension methods for the <see cref="Result{TValue}"/> class.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Ensures that the result meets the specified condition.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to check.</param>
    /// <param name="predicate">The condition to check the value against.</param>
    /// <param name="error">The error to return if the condition is not met.</param>
    /// <returns>The original result if the condition is met; otherwise, the specified error.</returns>
    public static Result<T> Ensure<T>(
        this Result<T> result,
        Func<T, bool> predicate,
        Error error)
    {
        if (result.IsFailure)
        {
            return result;
        }

        return predicate(result.Value)
            ? result
            : error;
    }

    /// <summary>
    /// Maps the value of a successful result to a new value.
    /// </summary>
    /// <typeparam name="TIn">The type of the input value.</typeparam>
    /// <typeparam name="TOut">The type of the output value.</typeparam>
    /// <param name="result">The result to map.</param>
    /// <param name="mappingFunc">The function to map the input value to the output value.</param>
    /// <returns>A new result with the mapped value if the original result is successful; otherwise, a failed result.</returns>
    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> mappingFunc)
    {
        return result.IsSuccess
            ? Result.Success(mappingFunc(result.Value))
            : Result.Failure<TOut>(result.Error);
    }
}

