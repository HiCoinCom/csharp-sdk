using ChainUpCustody.Config;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Client
{
  /// <summary>
  /// Factory for creating WaaS and MPC clients
  /// </summary>
  public static class WaasClientFactory
  {
    /// <summary>
    /// Creates a WaasClient builder
    /// </summary>
    /// <returns>A new WaasClientBuilder instance</returns>
    public static WaasClientBuilder CreateWaasClientBuilder()
    {
      return WaasClient.Builder();
    }

    /// <summary>
    /// Creates a MpcClient builder
    /// </summary>
    /// <returns>A new MpcClientBuilder instance</returns>
    public static MpcClientBuilder CreateMpcClientBuilder()
    {
      return MpcClient.Builder();
    }

    /// <summary>
    /// Creates a WaasClient with the specified configuration
    /// </summary>
    /// <param name="appId">Application ID</param>
    /// <param name="userPrivateKey">User private key (RSA)</param>
    /// <param name="waasPublicKey">WaaS public key (RSA)</param>
    /// <param name="domain">Optional custom domain</param>
    /// <param name="enableLog">Enable logging</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>A new WaasClient instance</returns>
    public static WaasClient CreateWaasClient(
        string appId,
        string userPrivateKey,
        string waasPublicKey,
        string? domain = null,
        bool enableLog = false,
        ILogger? logger = null)
    {
      var config = new WaasConfig
      {
        AppId = appId,
        UserPrivateKey = userPrivateKey,
        WaasPublicKey = waasPublicKey,
        EnableLog = enableLog
      };

      if (!string.IsNullOrEmpty(domain))
      {
        config.Domain = domain;
      }

      return new WaasClient(config, logger);
    }

    /// <summary>
    /// Creates a MpcClient with the specified configuration
    /// </summary>
    /// <param name="appId">Application ID</param>
    /// <param name="userPrivateKey">User private key (RSA)</param>
    /// <param name="waasPublicKey">WaaS public key (RSA)</param>
    /// <param name="signPrivateKey">Optional sign private key</param>
    /// <param name="domain">Optional custom domain</param>
    /// <param name="enableLog">Enable logging</param>
    /// <param name="logger">Optional logger</param>
    /// <returns>A new MpcClient instance</returns>
    public static MpcClient CreateMpcClient(
        string appId,
        string userPrivateKey,
        string waasPublicKey,
        string? signPrivateKey = null,
        string? domain = null,
        bool enableLog = false,
        ILogger? logger = null)
    {
      var config = new MpcConfig
      {
        AppId = appId,
        UserPrivateKey = userPrivateKey,
        WaasPublicKey = waasPublicKey,
        SignPrivateKey = signPrivateKey,
        EnableLog = enableLog
      };

      if (!string.IsNullOrEmpty(domain))
      {
        config.Domain = domain;
      }

      return new MpcClient(config, logger);
    }
  }
}
