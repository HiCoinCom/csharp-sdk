using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User account args
  /// </summary>
  public class UserAccountArgs : BaseArgs
  {
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public int? Uid { get; set; }

    /// <summary>
    /// Cryptocurrency symbol (e.g., BTC, ETH)
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }
  }
}
