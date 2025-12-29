using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Auto collection sub wallets info
  /// </summary>
  public class AutoCollectInfo
  {
    /// <summary>
    /// Fueling sub wallet ID (for fee refueling)
    /// </summary>
    [JsonProperty("fueling_sub_wallet_id")]
    public long? FuelingSubWalletId { get; set; }

    /// <summary>
    /// Collect sub wallet ID (for collection destination)
    /// </summary>
    [JsonProperty("collect_sub_wallet_id")]
    public long? CollectSubWalletId { get; set; }
  }

  /// <summary>
  /// Auto collection sub wallets result
  /// </summary>
  public class AutoCollectResult : Result<AutoCollectInfo>
  {
  }
}
