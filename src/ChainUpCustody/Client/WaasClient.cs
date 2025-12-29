using ChainUpCustody.Api.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Client
{
  /// <summary>
  /// WaaS client for ChainUp Custody API
  /// </summary>
  public class WaasClient
  {
    private readonly WaasConfig _config;
    private readonly IDataCrypto _dataCrypto;
    private readonly ILogger? _logger;

    /// <summary>
    /// User API
    /// </summary>
    public IUserApi UserApi { get; }

    /// <summary>
    /// Account API
    /// </summary>
    public IAccountApi AccountApi { get; }

    /// <summary>
    /// Coin API
    /// </summary>
    public ICoinApi CoinApi { get; }

    /// <summary>
    /// Billing API
    /// </summary>
    public IBillingApi BillingApi { get; }

    /// <summary>
    /// Transfer API
    /// </summary>
    public ITransferApi TransferApi { get; }

    /// <summary>
    /// Async Notify API
    /// </summary>
    public IAsyncNotifyApi AsyncNotifyApi { get; }

    /// <summary>
    /// Creates a new instance of WaasClient
    /// </summary>
    /// <param name="config">WaaS configuration</param>
    /// <param name="logger">Optional logger</param>
    public WaasClient(WaasConfig config, ILogger? logger = null)
    {
      _config = config;
      _logger = logger;
      _dataCrypto = new DataCrypto(config.UserPrivateKey, config.WaasPublicKey);

      // Initialize APIs
      UserApi = new UserApi(_config, _dataCrypto, _logger);
      AccountApi = new AccountApi(_config, _dataCrypto, _logger);
      CoinApi = new CoinApi(_config, _dataCrypto, _logger);
      BillingApi = new BillingApi(_config, _dataCrypto, _logger);
      TransferApi = new TransferApi(_config, _dataCrypto, _logger);
      AsyncNotifyApi = new AsyncNotifyApi(_config, _dataCrypto, _logger);
    }

    /// <summary>
    /// Creates a new WaasClient builder
    /// </summary>
    /// <returns>A new WaasClientBuilder instance</returns>
    public static WaasClientBuilder Builder()
    {
      return new WaasClientBuilder();
    }
  }

  /// <summary>
  /// Builder for WaasClient
  /// </summary>
  public class WaasClientBuilder
  {
    private string? _appId;
    private string? _userPrivateKey;
    private string? _waasPublicKey;
    private string? _domain;
    private bool _enableLog;
    private ILogger? _logger;

    /// <summary>
    /// Sets the application ID
    /// </summary>
    public WaasClientBuilder SetAppId(string appId)
    {
      _appId = appId;
      return this;
    }

    /// <summary>
    /// Sets the user private key
    /// </summary>
    public WaasClientBuilder SetUserPrivateKey(string userPrivateKey)
    {
      _userPrivateKey = userPrivateKey;
      return this;
    }

    /// <summary>
    /// Sets the WaaS public key
    /// </summary>
    public WaasClientBuilder SetWaasPublicKey(string waasPublicKey)
    {
      _waasPublicKey = waasPublicKey;
      return this;
    }

    /// <summary>
    /// Sets the API domain
    /// </summary>
    public WaasClientBuilder SetDomain(string domain)
    {
      _domain = domain;
      return this;
    }

    /// <summary>
    /// Enables logging
    /// </summary>
    public WaasClientBuilder EnableLog(bool enable = true)
    {
      _enableLog = enable;
      return this;
    }

    /// <summary>
    /// Sets the logger
    /// </summary>
    public WaasClientBuilder SetLogger(ILogger logger)
    {
      _logger = logger;
      return this;
    }

    /// <summary>
    /// Builds the WaasClient
    /// </summary>
    /// <returns>A new WaasClient instance</returns>
    public WaasClient Build()
    {
      var config = new WaasConfig
      {
        AppId = _appId!,
        UserPrivateKey = _userPrivateKey!,
        WaasPublicKey = _waasPublicKey!,
        EnableLog = _enableLog
      };

      if (!string.IsNullOrEmpty(_domain))
      {
        config.Domain = _domain;
      }

      return new WaasClient(config, _logger);
    }
  }
}
