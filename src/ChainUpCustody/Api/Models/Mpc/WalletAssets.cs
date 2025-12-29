using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Wallet assets entity
  /// </summary>
  public class WalletAssets
  {
    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("normal_balance")]
    public string? NormalBalance { get; set; }

    /// <summary>
    /// Freeze balance
    /// </summary>
    [JsonProperty("lock_balance")]
    public string? LockBalance { get; set; }

    /// <summary>
    /// Balance for awaiting consolidation assets
    /// </summary>
    [JsonProperty("collecting_balance")]
    public string? CollectingBalance { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("deposit_address")]
    public string? DepositAddress { get; set; }
  }
}
