using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Transfer list query args
  /// </summary>
  public class TransferListArgs : BaseArgs
  {
    /// <summary>
    /// Comma-separated ID list
    /// </summary>
    [JsonProperty("ids")]
    public string? Ids { get; set; }

    /// <summary>
    /// Type of IDs: "request_id" or "receipt"
    /// </summary>
    [JsonProperty("ids_type")]
    public string? IdsType { get; set; }
  }
}
