using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Wallet entity
  /// </summary>
  public class Wallet
  {
    /// <summary>
    /// Wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int SubWalletId { get; set; }
  }
}
