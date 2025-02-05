namespace Bieber.Core.Utilities.Errors;
/// <summary>
/// Enumerates different types of errors.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// General error that is not specifically categorized.
    /// </summary>
    Failure = 0,

    /// <summary>
    /// <b>Validation:</b> Error in validating user input or data.
    /// </summary>
    Validation = 1,

    /// <summary>
    /// <b>Problem:</b> A known problem or error state.
    /// </summary>
    Problem = 2,

    /// <summary>
    /// <b>BadRequest:</b> Invalid or poorly formatted request to the server.
    /// </summary>
    BadRequest = 3,

    /// <summary>
    /// <b>NotFound:</b> Requested resource or data not found.
    /// </summary>
    NotFound = 4,

    /// <summary>
    /// <b>Forbidden:</b> Access to the requested resource is forbidden.
    /// </summary>
    Forbidden = 5,

    /// <summary>
    /// <b>Conflict:</b> Conflict in the request, such as duplicate resources.
    /// </summary>
    Conflict = 6,

    /// <summary>
    /// <b>Authentication:</b> Authentication error, invalid credentials.
    /// </summary>
    Authentication = 7,

    /// <summary>
    /// <b>Authorization:</b> Authorization error, user lacks permission.
    /// </summary>
    Authorization = 8,

    /// <summary>
    /// <b>Timeout:</b> Request timeout, the server did not respond in time.
    /// </summary>
    Timeout = 9,

    /// <summary>
    /// <b>ServiceUnavailable:</b> The requested service is temporarily unavailable.
    /// </summary>
    ServiceUnavailable = 10
}
