using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Web3 transaction entity
  /// </summary>
  public class Web3Transaction
  {
    /// <summary>
    /// Transaction ID
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }
  }
}
