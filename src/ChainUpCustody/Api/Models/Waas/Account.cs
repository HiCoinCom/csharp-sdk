using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Company/Merchant account entity
  /// </summary>
  public class Account
  {
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("balance")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Frozen balance
    /// </summary>
    [JsonProperty("frozen")]
    public decimal? Frozen { get; set; }
  }
}
