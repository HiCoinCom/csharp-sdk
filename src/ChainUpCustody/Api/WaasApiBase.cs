using System;
using ChainUpCustody.Api.Models;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using ChainUpCustody.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChainUpCustody.Api
{
  /// <summary>
  /// Base WaaS API class with synchronous invoke methods
  /// </summary>
  public abstract class WaasApiBase
  {
    protected readonly WaasConfig Config;
    protected readonly IDataCrypto DataCrypto;
    protected readonly ILogger? Logger;

    protected WaasApiBase(WaasConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
    {
      Config = config ?? throw new ArgumentNullException(nameof(config));
      DataCrypto = dataCrypto ?? throw new ArgumentNullException(nameof(dataCrypto));
      Logger = logger;
    }

    /// <summary>
    /// Invoke a WaaS API endpoint
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="apiUri">The API URI enum value</param>
    /// <param name="args">Request args</param>
    /// <returns>The API response result</returns>
    protected T Invoke<T>(ApiUri apiUri, BaseArgs args) where T : class, new()
    {
      return Invoke<T>(apiUri.Value, apiUri.Method, args);
    }

    /// <summary>
    /// Invoke an MPC API endpoint
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="apiUri">The MPC API URI enum value</param>
    /// <param name="args">Request args</param>
    /// <returns>The API response result</returns>
    protected T Invoke<T>(MpcApiUri apiUri, BaseArgs args) where T : class, new()
    {
      return Invoke<T>(apiUri.Value, apiUri.Method, args);
    }

    /// <summary>
    /// Invoke an API endpoint
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="uri">API path</param>
    /// <param name="method">HTTP method</param>
    /// <param name="args">Request args</param>
    /// <returns>The API response result</returns>
    protected T Invoke<T>(string uri, string method, BaseArgs args) where T : class, new()
    {
      try
      {
        // Set default parameters
        args.Charset = Config.Charset;
        args.Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // Serialize args to JSON
        var raw = args.ToJson();
        LogInfo("Request API: {0}, Request args: {1}", uri, raw);

        // Encrypt the data
        var encryptedData = DataCrypto.Encode(raw);
        LogInfo("Request API: {0}, Encoded args: {1}", uri, encryptedData);

        if (string.IsNullOrEmpty(encryptedData))
        {
          Logger?.LogError("Request API: {0}, encode args return null", uri);
          throw new Exception("Data crypto return null");
        }

        // Build request parameters
        var requestArgs = new Args(Config.AppId, encryptedData);

        // Build URL
        var url = $"{Config.Domain}{uri}";

        // Send request
        string responseBody;
        if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
          url += "?" + requestArgs.ToQueryString();
          responseBody = HttpClientUtil.Get(url, Logger);
        }
        else
        {
          responseBody = HttpClientUtil.Post(url, requestArgs.ToMap(), Logger);
        }

        LogInfo("Request API: {0}, Raw result: {1}", uri, responseBody);

        if (string.IsNullOrEmpty(responseBody))
        {
          Logger?.LogError("Request API: {0}, API return null", uri);
          return new T();
        }

        // Parse response
        var jsonResponse = JObject.Parse(responseBody);
        var dataToken = jsonResponse["data"];
        if (dataToken == null)
        {
          Logger?.LogError("Request API: {0}, result does not contain data field", uri);
          return new T();
        }

        // Decrypt response data
        string? dataValue = dataToken.ToString();
        if (string.IsNullOrEmpty(dataValue))
        {
          Logger?.LogError("Request API: {0}, data field is empty", uri);
          return new T();
        }

        var decryptedData = DataCrypto.Decode(dataValue);
        LogInfo("Request API: {0}, Decoded result: {1}", uri, decryptedData);

        if (string.IsNullOrEmpty(decryptedData))
        {
          Logger?.LogError("Request API: {0}, decode result return null", uri);
          return new T();
        }

        // Deserialize to result type
        var result = JsonConvert.DeserializeObject<T>(decryptedData);
        if (result == null)
        {
          Logger?.LogError("Request API: {0}, result parse json to object error, json: {1}", uri, decryptedData);
          return new T();
        }

        return result;
      }
      catch (Exception ex)
      {
        Logger?.LogError(ex, "API invoke failed: {0}", uri);
        throw;
      }
    }

    /// <summary>
    /// Log information if logging is enabled
    /// </summary>
    protected void LogInfo(string format, params object[] args)
    {
      if (Config.EnableLog)
      {
        Logger?.LogInformation(format, args);
      }
    }
  }
}
