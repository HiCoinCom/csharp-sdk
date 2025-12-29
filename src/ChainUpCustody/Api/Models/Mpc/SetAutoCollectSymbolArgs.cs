using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Parameters for setting auto-collection symbol configuration
  /// </summary>
  public class SetAutoCollectSymbolArgs : BaseArgs
  {
    /// <summary>
    /// Unique identifier for the coin (e.g., "USDTERC20")
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Minimum amount for auto-sweep (up to 6 decimal places)
    /// </summary>
    [JsonProperty("collect_min")]
    public string? CollectMin { get; set; }

    /// <summary>
    /// Maximum miner fee amount for auto-sweep refueling (up to 6 decimal places)
    /// </summary>
    [JsonProperty("fueling_limit")]
    public string? FuelingLimit { get; set; }
  }
}
