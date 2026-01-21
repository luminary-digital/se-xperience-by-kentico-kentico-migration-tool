using System.Text;

namespace Migration.Tool.Common.Helpers;

public static class UriHelper
{
    public static string BuildXbyKDomainString(Uri uri, int expectedSize)
    {
        var sb = new StringBuilder(expectedSize);
        sb.Append(uri.Host);
        if (!uri.IsDefaultPort)
        {
            sb.Append($":{uri.Port}");
        }

        if (uri.AbsolutePath != "/")
        {
            sb.Append(uri.AbsolutePath);
        }

        // Ensure the domain string doesn't end with a trailing slash
        string result = sb.ToString().TrimEnd('/');
        
        // Validate: must contain at least one dot unless it's exactly "localhost"
        if (result != "localhost" && !result.Contains('.'))
        {
            // If no dot found and not localhost, append .local to make it valid
            result = result.Split(':')[0] + ".local" + (result.Contains(':') ? result.Substring(result.IndexOf(':')) : "");
        }

        return result;
    }

    public static UniqueDomainResult GetUniqueDomainCandidate(string input, ref int startPort, Func<string, bool> checkIsUnique, int maxAttempts = 100)
    {
        bool useFallback = false;
        string initial = input;
        if (string.IsNullOrWhiteSpace(input))
        {
            initial = "https://localhost";
            useFallback = true;
        }

        if (!initial.Contains("//"))
        {
            initial = $"https://{initial}";
        }

        if (!Uri.TryCreate(initial, UriKind.Absolute, out var uriTmp))
        {
            initial = "https://localhost";
            useFallback = true;
        }

        var uri = uriTmp ?? new Uri("https://localhost");

        bool changed = false;
        string candidate = BuildXbyKDomainString(uri, initial.Length + 20);
        while (!checkIsUnique(candidate) && --maxAttempts > 0)
        {
            var builder = new UriBuilder(uri) { Port = ++startPort, Scheme = "https" };
            candidate = BuildXbyKDomainString(builder.Uri, initial.Length + 20);
            changed = true;
        }

        if (maxAttempts <= 0)
        {
            return new UniqueDomainResult(false, changed, input, null);
        }

        return useFallback
            ? new UniqueDomainResult(!useFallback, changed, input, candidate)
            : new UniqueDomainResult(!useFallback, changed, candidate, null);
    }

    public record struct UniqueDomainResult(bool Success, bool Changed, string Result, string? Fallback);
}
