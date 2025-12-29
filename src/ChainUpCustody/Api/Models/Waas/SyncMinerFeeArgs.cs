using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Sync miner fee args
  /// </summary>
  public class SyncMinerFeeArgs : BaseArgs
  {
    /// <summary>
    /// Max ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public int MaxId { get; set; }
  }
}
