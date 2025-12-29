using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Sync withdraw record args
  /// </summary>
  public class SyncWithdrawRecordArgs : BaseArgs
  {
    /// <summary>
    /// Maximum ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public long? MaxId { get; set; }
  }
}
