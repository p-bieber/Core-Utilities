namespace Bieber.Core.Utilities.Errors;

using System.Collections.Generic;
using System.Globalization;
using System.Resources;

/// <summary>
/// Static class for managing error resources.
/// </summary>
public static class ErrorResourceManager
{
    private static readonly List<ResourceManager> ResourceManagers = [];

    /// <summary>
    /// Registers a new ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to register.</param>
    public static void RegisterResourceManager(ResourceManager resourceManager)
    {
        ResourceManagers.Add(resourceManager);
    }

    /// <summary>
    /// Returns the localized string for the specified key.
    /// </summary>
    /// <param name="key">The key of the string to look up.</param>
    /// <returns>The localized string or an error message if the key is not found.</returns>
    public static string GetString(string key)
    {
        foreach (var resourceManager in ResourceManagers)
        {
            string? result = resourceManager.GetString(key, CultureInfo.CurrentUICulture);
            if (result != null)
            {
                return result;
            }
        }
        return $"Missing localization for key: {key}";
    }
}
