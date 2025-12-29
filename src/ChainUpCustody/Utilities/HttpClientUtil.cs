using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Utilities
{
  /// <summary>
  /// HTTP client utility class for making API requests
  /// </summary>
  public static class HttpClientUtil
  {
    private static readonly HttpClient _httpClient;

    static HttpClientUtil()
    {
      _httpClient = new HttpClient
      {
        Timeout = TimeSpan.FromSeconds(30)
      };
    }

    /// <summary>
    /// Send a POST request with form data (synchronous)
    /// </summary>
    /// <param name="url">The URL to send the request to</param>
    /// <param name="parameters">The form parameters</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>The response body as a string</returns>
    public static string Post(string url, Dictionary<string, string> parameters, ILogger? logger = null)
    {
      return PostAsync(url, parameters, logger).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Send a GET request (synchronous)
    /// </summary>
    /// <param name="url">The full URL with query string</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>The response body as a string</returns>
    public static string Get(string url, ILogger? logger = null)
    {
      return GetAsync(url, new Dictionary<string, string>(), logger).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Send a POST request with form data
    /// </summary>
    /// <param name="url">The URL to send the request to</param>
    /// <param name="parameters">The form parameters</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>The response body as a string</returns>
    public static async Task<string> PostAsync(string url, Dictionary<string, string> parameters, ILogger? logger = null)
    {
      try
      {
        logger?.LogDebug("POST Request URL: {Url}", url);
        logger?.LogDebug("POST Request Parameters: {Parameters}", string.Join(", ", parameters));

        var content = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        logger?.LogDebug("POST Response: {Response}", responseBody);

        return responseBody;
      }
      catch (Exception ex)
      {
        logger?.LogError(ex, "POST request failed: {Url}", url);
        throw;
      }
    }

    /// <summary>
    /// Send a GET request with query parameters
    /// </summary>
    /// <param name="url">The base URL</param>
    /// <param name="parameters">The query parameters</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>The response body as a string</returns>
    public static async Task<string> GetAsync(string url, Dictionary<string, string> parameters, ILogger? logger = null)
    {
      try
      {
        var queryString = BuildQueryString(parameters);
        var fullUrl = string.IsNullOrEmpty(queryString) ? url : $"{url}?{queryString}";

        logger?.LogDebug("GET Request URL: {Url}", fullUrl);

        var response = await _httpClient.GetAsync(fullUrl);

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        logger?.LogDebug("GET Response: {Response}", responseBody);

        return responseBody;
      }
      catch (Exception ex)
      {
        logger?.LogError(ex, "GET request failed: {Url}", url);
        throw;
      }
    }

    /// <summary>
    /// Build a query string from dictionary parameters
    /// </summary>
    private static string BuildQueryString(Dictionary<string, string> parameters)
    {
      if (parameters == null || parameters.Count == 0)
      {
        return string.Empty;
      }

      var sb = new StringBuilder();
      foreach (var kvp in parameters)
      {
        if (sb.Length > 0)
        {
          sb.Append('&');
        }
        sb.Append(HttpUtility.UrlEncode(kvp.Key));
        sb.Append('=');
        sb.Append(HttpUtility.UrlEncode(kvp.Value));
      }
      return sb.ToString();
    }
  }
}
