using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Sync deposit record args
  /// </summary>
  public class SyncDepositRecordArgs : BaseArgs
  {
    /// <summary>
    /// Maximum ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public long? MaxId { get; set; }
  }
}
