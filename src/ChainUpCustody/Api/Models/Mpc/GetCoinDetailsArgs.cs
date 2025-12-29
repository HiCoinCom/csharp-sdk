using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get coin details args
  /// </summary>
  public class GetCoinDetailsArgs : BaseArgs
  {
    /// <summary>
    /// Unique identifier for the coin, used for transfers, e.g.: USDTERC20
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Main chain coins, Unique identifier for the coin, e.g.: ETH
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Main chain coin, default to getting all, true to get opened coins, false to get unopened coins
    /// </summary>
    [JsonProperty("open_chain")]
    public bool? OpenChain { get; set; }

    /// <summary>
    /// Starting id of the currency, If not passed, the default return is the latest 1500 currencies
    /// </summary>
    [JsonProperty("max_id")]
    public int? MaxId { get; set; }

    /// <summary>
    /// The number of currencies to be obtained each time. If not passed, the default value is 1500
    /// </summary>
    [JsonProperty("limit")]
    public int? Limit { get; set; }
  }
}
