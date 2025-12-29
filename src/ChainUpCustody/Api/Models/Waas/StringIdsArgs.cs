using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// String IDs args (for batch query)
  /// </summary>
  public class StringIdsArgs : BaseArgs
  {
    /// <summary>
    /// Comma-separated ID list
    /// </summary>
    [JsonProperty("ids")]
    public string? IdList { get; set; }
  }
}
