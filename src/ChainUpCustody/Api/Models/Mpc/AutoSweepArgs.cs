using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Auto sweep args
  /// </summary>
  public class AutoSweepArgs : BaseArgs
  {
    /// <summary>
    /// Sub wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int? SubWalletId { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Max gas price
    /// </summary>
    [JsonProperty("max_gas_price")]
    public string? MaxGasPrice { get; set; }
  }
}
