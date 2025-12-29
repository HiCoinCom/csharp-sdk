using ChainUpCustody.Api.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Client
{
  /// <summary>
  /// MPC client for ChainUp Custody API
  /// </summary>
  public class MpcClient
  {
    private readonly MpcConfig _config;
    private readonly IDataCrypto _dataCrypto;
    private readonly ILogger? _logger;

    /// <summary>
    /// Workspace API
    /// </summary>
    public IWorkSpaceApi WorkSpaceApi { get; }

    /// <summary>
    /// Wallet API
    /// </summary>
    public IWalletApi WalletApi { get; }

    /// <summary>
    /// Deposit API
    /// </summary>
    public IDepositApi DepositApi { get; }

    /// <summary>
    /// Withdraw API
    /// </summary>
    public IWithdrawApi WithdrawApi { get; }

    /// <summary>
    /// Web3 API
    /// </summary>
    public IWeb3Api Web3Api { get; }

    /// <summary>
    /// Auto Sweep API
    /// </summary>
    public IAutoSweepApi AutoSweepApi { get; }

    /// <summary>
    /// Notify API
    /// </summary>
    public INotifyApi NotifyApi { get; }

    /// <summary>
    /// Tron Buy Resource API
    /// </summary>
    public ITronBuyResourceApi TronBuyResourceApi { get; }

    /// <summary>
    /// Creates a new instance of MpcClient
    /// </summary>
    /// <param name="config">MPC configuration</param>
    /// <param name="logger">Optional logger</param>
    public MpcClient(MpcConfig config, ILogger? logger = null)
    {
      _config = config;
      _logger = logger;

      // Create data crypto with sign key if available
      if (!string.IsNullOrEmpty(config.SignPrivateKey))
      {
        _dataCrypto = new DataCrypto(config.UserPrivateKey, config.WaasPublicKey, config.SignPrivateKey);
      }
      else
      {
        _dataCrypto = new DataCrypto(config.UserPrivateKey, config.WaasPublicKey);
      }

      // Initialize APIs
      WorkSpaceApi = new WorkSpaceApi(_config, _dataCrypto, _logger);
      WalletApi = new WalletApi(_config, _dataCrypto, _logger);
      DepositApi = new DepositApi(_config, _dataCrypto, _logger);
      WithdrawApi = new WithdrawApi(_config, _dataCrypto, _logger);
      Web3Api = new Web3Api(_config, _dataCrypto, _logger);
      AutoSweepApi = new AutoSweepApi(_config, _dataCrypto, _logger);
      NotifyApi = new NotifyApi(_config, _dataCrypto, _logger);
      TronBuyResourceApi = new TronBuyResourceApi(_config, _dataCrypto, _logger);
    }

    /// <summary>
    /// Creates a new MpcClient builder
    /// </summary>
    /// <returns>A new MpcClientBuilder instance</returns>
    public static MpcClientBuilder Builder()
    {
      return new MpcClientBuilder();
    }
  }

  /// <summary>
  /// Builder for MpcClient
  /// </summary>
  public class MpcClientBuilder
  {
    private string? _appId;
    private string? _userPrivateKey;
    private string? _waasPublicKey;
    private string? _signPrivateKey;
    private string? _domain;
    private bool _enableLog;
    private ILogger? _logger;

    /// <summary>
    /// Sets the application ID
    /// </summary>
    public MpcClientBuilder SetAppId(string appId)
    {
      _appId = appId;
      return this;
    }

    /// <summary>
    /// Sets the user private key
    /// </summary>
    public MpcClientBuilder SetUserPrivateKey(string userPrivateKey)
    {
      _userPrivateKey = userPrivateKey;
      return this;
    }

    /// <summary>
    /// Sets the WaaS public key
    /// </summary>
    public MpcClientBuilder SetWaasPublicKey(string waasPublicKey)
    {
      _waasPublicKey = waasPublicKey;
      return this;
    }

    /// <summary>
    /// Sets the sign private key
    /// </summary>
    public MpcClientBuilder SetSignPrivateKey(string signPrivateKey)
    {
      _signPrivateKey = signPrivateKey;
      return this;
    }

    /// <summary>
    /// Sets the API domain
    /// </summary>
    public MpcClientBuilder SetDomain(string domain)
    {
      _domain = domain;
      return this;
    }

    /// <summary>
    /// Enables logging
    /// </summary>
    public MpcClientBuilder EnableLog(bool enable = true)
    {
      _enableLog = enable;
      return this;
    }

    /// <summary>
    /// Sets the logger
    /// </summary>
    public MpcClientBuilder SetLogger(ILogger logger)
    {
      _logger = logger;
      return this;
    }

    /// <summary>
    /// Builds the MpcClient
    /// </summary>
    /// <returns>A new MpcClient instance</returns>
    public MpcClient Build()
    {
      var config = new MpcConfig
      {
        AppId = _appId!,
        UserPrivateKey = _userPrivateKey!,
        WaasPublicKey = _waasPublicKey!,
        SignPrivateKey = _signPrivateKey!,
        EnableLog = _enableLog
      };

      if (!string.IsNullOrEmpty(_domain))
      {
        config.Domain = _domain;
      }

      return new MpcClient(config, _logger);
    }
  }
}
