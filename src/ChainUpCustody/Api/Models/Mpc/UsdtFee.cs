using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// USDT fee entity
  /// </summary>
  public class UsdtFee
  {
    /// <summary>
    /// Estimated energy
    /// </summary>
    [JsonProperty("energy")]
    public long Energy { get; set; }

    /// <summary>
    /// Estimated fee
    /// </summary>
    [JsonProperty("fee")]
    public string? Fee { get; set; }
  }
}
