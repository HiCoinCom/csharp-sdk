using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Query energy order args
  /// </summary>
  public class QueryEnergyOrderArgs : BaseArgs
  {
    /// <summary>
    /// Order IDs (comma separated)
    /// </summary>
    [JsonProperty("ids")]
    public string? Ids { get; set; }

    /// <summary>
    /// Order number
    /// </summary>
    [JsonProperty("order_no")]
    public string? OrderNo { get; set; }

    /// <summary>
    /// Request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }
  }
}
