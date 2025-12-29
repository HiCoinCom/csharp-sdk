using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Estimate USDT fee args
  /// </summary>
  public class EstimateUsdtFeeArgs : BaseArgs
  {
    /// <summary>
    /// From address
    /// </summary>
    [JsonProperty("from")]
    public string? From { get; set; }

    /// <summary>
    /// To address
    /// </summary>
    [JsonProperty("to")]
    public string? To { get; set; }
  }
}
