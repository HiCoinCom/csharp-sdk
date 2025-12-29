using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Company/Merchant account entity
  /// </summary>
  public class Account
  {
    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }
  }
}
