using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Query wallet address args
  /// </summary>
  public class QueryAddressArgs : BaseArgs
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

    /// <summary>
    /// Max ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public int? MaxId { get; set; }
  }
}
