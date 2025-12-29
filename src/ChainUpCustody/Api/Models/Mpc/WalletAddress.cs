using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Wallet address entity
  /// </summary>
  public class WalletAddress
  {
    /// <summary>
    /// Address ID
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Memo/Tag
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }
  }
}
