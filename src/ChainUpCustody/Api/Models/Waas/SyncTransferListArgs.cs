using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Sync transfer list args
  /// </summary>
  public class SyncTransferListArgs : BaseArgs
  {
    /// <summary>
    /// Max ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public int? MaxId { get; set; }
  }
}
