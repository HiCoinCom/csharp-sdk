using System;

namespace ChainUpCustody.Config
{
  /// <summary>
  /// Configuration class for MPC client
  /// This class extends WaasConfig and adds MPC-specific configuration
  /// parameters. It includes an additional signing private key used for
  /// enhanced security in withdrawal and Web3 transaction operations.
  /// </summary>
  public class MpcConfig : WaasConfig
  {
    /// <summary>
    /// The RSA private key used for encryption parameters when withdrawing or creating web3 transactions
    /// </summary>
    public string? SignPrivateKey { get; set; }

    /// <summary>
    /// Custody domain name (overrides WaasConfig default)
    /// </summary>
    public override string Domain { get; set; } = "https://openapi.chainup.com";

    /// <summary>
    /// Character encoding for requests and responses
    /// </summary>
    public override string Charset { get; set; } = "utf-8";

    /// <summary>
    /// Flag to enable/disable logging
    /// </summary>
    public override bool EnableLog { get; set; } = false;
  }
}
