using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Query energy price args
  /// </summary>
  public class QueryEnergyPriceArgs : BaseArgs
  {
    /// <summary>
    /// Period (in days, can use decimals)
    /// </summary>
    [JsonProperty("period")]
    public string? Period { get; set; }
  }
}
