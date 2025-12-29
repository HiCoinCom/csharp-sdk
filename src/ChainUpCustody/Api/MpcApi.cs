using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChainUpCustody.Api.Models;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using ChainUpCustody.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChainUpCustody.Api
{
  /// <summary>
  /// Base MPC API class with invoke methods and signature support
  /// </summary>
  public abstract class MpcApi
  {
    protected readonly MpcConfig Config;
    protected readonly IDataCrypto DataCrypto;
    protected readonly ILogger? Logger;

    protected MpcApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
    {
      Config = config ?? throw new ArgumentNullException(nameof(config));
      DataCrypto = dataCrypto ?? throw new ArgumentNullException(nameof(dataCrypto));
      Logger = logger;
    }

    /// <summary>
    /// Invoke a MPC API endpoint with dictionary parameters
    /// </summary>
    /// <param name="apiUri">The API URI enum value</param>
    /// <param name="bizContent">The business content parameters</param>
    /// <returns>The API response result</returns>
    protected async Task<Result<T>> InvokeAsync<T>(MpcApiUri apiUri, Dictionary<string, object> bizContent)
    {
      try
      {
        var jsonContent = JsonConvert.SerializeObject(bizContent);
        return await InvokeAsync<T>(apiUri, jsonContent);
      }
      catch (Exception ex)
      {
        Logger?.LogError(ex, "MPC API invoke failed: {ApiUri}", apiUri.Value);
        return Result<T>.Error(-1, ex.Message);
      }
    }

    /// <summary>
    /// Invoke a MPC API endpoint with JSON string
    /// </summary>
    /// <param name="apiUri">The API URI enum value</param>
    /// <param name="bizContent">The business content as JSON string</param>
    /// <returns>The API response result</returns>
    protected async Task<Result<T>> InvokeAsync<T>(MpcApiUri apiUri, string bizContent)
    {
      try
      {
        if (Config.EnableLog)
        {
          Logger?.LogInformation("Request MPC API: {ApiUri}, Content: {Content}", apiUri.Value, bizContent);
        }

        // Encrypt the business content
        var encryptedData = DataCrypto.Encode(bizContent);

        // Build request parameters
        var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var args = new Args
        {
          AppId = Config.AppId,
          Data = encryptedData,
          Time = time,
          Sign = ""
        };
        var parameters = args.ToDictionary();

        // Build URL
        var url = $"{Config.Domain}{apiUri.Value}";

        // Send request
        string responseBody;
        if (apiUri.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
          responseBody = await HttpClientUtil.GetAsync(url, parameters, Logger);
        }
        else
        {
          responseBody = await HttpClientUtil.PostAsync(url, parameters, Logger);
        }

        // Parse response
        var result = JsonConvert.DeserializeObject<Result<string>>(responseBody);

        if (result == null)
        {
          return Result<T>.Error(-1, "Failed to parse response");
        }

        if (!result.IsSuccess)
        {
          return Result<T>.Error(result.Code, result.Msg);
        }

        // Decrypt response data if exists
        if (!string.IsNullOrEmpty(result.Data))
        {
          var decryptedData = DataCrypto.Decode(result.Data);

          if (Config.EnableLog)
          {
            Logger?.LogInformation("MPC Response Data: {Data}", decryptedData);
          }

          var data = JsonConvert.DeserializeObject<T>(decryptedData);
          return Result<T>.Success(data!);
        }

        return Result<T>.Success(default!);
      }
      catch (Exception ex)
      {
        Logger?.LogError(ex, "MPC API invoke failed: {ApiUri}", apiUri.Value);
        return Result<T>.Error(-1, ex.Message);
      }
    }

    /// <summary>
    /// Invoke a MPC API endpoint and return raw string result
    /// </summary>
    protected async Task<Result<string>> InvokeRawAsync(MpcApiUri apiUri, Dictionary<string, object> bizContent)
    {
      try
      {
        var jsonContent = JsonConvert.SerializeObject(bizContent);
        return await InvokeRawAsync(apiUri, jsonContent);
      }
      catch (Exception ex)
      {
        Logger?.LogError(ex, "MPC API invoke failed: {ApiUri}", apiUri.Value);
        return Result<string>.Error(-1, ex.Message);
      }
    }

    /// <summary>
    /// Invoke a MPC API endpoint and return raw string result
    /// </summary>
    protected async Task<Result<string>> InvokeRawAsync(MpcApiUri apiUri, string bizContent)
    {
      try
      {
        if (Config.EnableLog)
        {
          Logger?.LogInformation("Request MPC API: {ApiUri}, Content: {Content}", apiUri.Value, bizContent);
        }

        // Encrypt the business content
        var encryptedData = DataCrypto.Encode(bizContent);

        // Build request parameters
        var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var signContent = $"{Config.AppId}{encryptedData}{time}";
        var sign = DataCrypto.HasSignKey() ? DataCrypto.Sign(signContent) : null;

        var args = new Args
        {
          AppId = Config.AppId,
          Data = encryptedData,
          Time = time,
          Sign = sign
        };
        var parameters = args.ToDictionary();

        // Build URL
        var url = $"{Config.Domain}{apiUri.Value}";

        // Send request
        string responseBody;
        if (apiUri.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
          responseBody = await HttpClientUtil.GetAsync(url, parameters, Logger);
        }
        else
        {
          responseBody = await HttpClientUtil.PostAsync(url, parameters, Logger);
        }

        // Parse response
        var result = JsonConvert.DeserializeObject<Result<string>>(responseBody);

        if (result == null)
        {
          return Result<string>.Error(-1, "Failed to parse response");
        }

        if (!result.IsSuccess)
        {
          return Result<string>.Error(result.Code, result.Msg);
        }

        // Decrypt response data if exists
        if (!string.IsNullOrEmpty(result.Data))
        {
          var decryptedData = DataCrypto.Decode(result.Data);

          if (Config.EnableLog)
          {
            Logger?.LogInformation("MPC Response Data: {Data}", decryptedData);
          }

          return Result<string>.Success(decryptedData);
        }

        return Result<string>.Success(string.Empty);
      }
      catch (Exception ex)
      {
        Logger?.LogError(ex, "MPC API invoke failed: {ApiUri}", apiUri.Value);
        return Result<string>.Error(-1, ex.Message);
      }
    }
  }
}
