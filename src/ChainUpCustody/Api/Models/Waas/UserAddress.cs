using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User address entity
  /// </summary>
  public class UserAddress
  {
    /// <summary>
    /// Address ID (from sync list)
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long? Uid { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }
  }
}
