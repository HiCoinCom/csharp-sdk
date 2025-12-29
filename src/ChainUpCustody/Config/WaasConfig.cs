using System;

namespace ChainUpCustody.Config
{
  /// <summary>
  /// Configuration class for WaaS client
  /// This class holds all necessary configuration parameters for connecting
  /// to and authenticating with the ChainUp WaaS service.
  /// </summary>
  public class WaasConfig
  {
    /// <summary>
    /// Custody appId
    /// </summary>
    public required string AppId { get; set; }

    /// <summary>
    /// Customer RSA private key
    /// </summary>
    public required string UserPrivateKey { get; set; }

    /// <summary>
    /// The RSA public key provided by Custody
    /// </summary>
    public required string WaasPublicKey { get; set; }

    /// <summary>
    /// Custody domain name
    /// </summary>
    public virtual string Domain { get; set; } = "https://openapi.chainup.com/api/v2";

    /// <summary>
    /// API version
    /// </summary>
    public string Version { get; set; } = "v2";

    /// <summary>
    /// Character encoding for requests and responses
    /// </summary>
    public virtual string Charset { get; set; } = "utf-8";

    /// <summary>
    /// Flag to enable/disable logging
    /// </summary>
    public virtual bool EnableLog { get; set; } = false;
  }
}
