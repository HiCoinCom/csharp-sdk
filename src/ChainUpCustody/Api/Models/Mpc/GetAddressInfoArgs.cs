using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get address info args
  /// </summary>
  public class GetAddressInfoArgs : BaseArgs
  {
    /// <summary>
    /// Blockchain address
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Memo (for memo-type chains)
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }
  }
}
