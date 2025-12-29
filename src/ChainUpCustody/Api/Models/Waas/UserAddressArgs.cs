using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User address args
  /// </summary>
  public class UserAddressArgs : BaseArgs
  {
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public int? Uid { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Blockchain address
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }
  }
}
