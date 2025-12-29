using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Parameters for syncing auto-collect records
  /// </summary>
  public class SyncAutoCollectRecordsArgs : BaseArgs
  {
    /// <summary>
    /// Starting ID for sweeping records (default: 0)
    /// </summary>
    [JsonProperty("max_id")]
    public long? MaxId { get; set; }
  }
}
