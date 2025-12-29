using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Sync auto sweep record args
  /// </summary>
  public class SyncAutoSweepRecordArgs : BaseArgs
  {
    /// <summary>
    /// Maximum ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public long? MaxId { get; set; }
  }
}
