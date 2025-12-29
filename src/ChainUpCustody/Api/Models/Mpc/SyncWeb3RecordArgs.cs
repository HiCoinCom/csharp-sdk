using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Sync Web3 record args
  /// </summary>
  public class SyncWeb3RecordArgs : BaseArgs
  {
    /// <summary>
    /// Maximum ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public long? MaxId { get; set; }
  }
}
