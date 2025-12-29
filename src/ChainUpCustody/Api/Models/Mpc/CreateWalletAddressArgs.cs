using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Create wallet address args
  /// </summary>
  public class CreateWalletAddressArgs : BaseArgs
  {
    /// <summary>
    /// Wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int? SubWalletId { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }
  }
}
