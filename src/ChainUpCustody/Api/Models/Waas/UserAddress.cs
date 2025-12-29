using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User address entity
  /// </summary>
  public class UserAddress
  {
    /// <summary>
    /// ID
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public int Uid { get; set; }

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

    /// <summary>
    /// Address tag/memo
    /// </summary>
    [JsonProperty("address_tag")]
    public string? AddressTag { get; set; }
  }
}
