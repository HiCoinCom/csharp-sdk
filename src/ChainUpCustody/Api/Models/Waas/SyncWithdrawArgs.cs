using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Sync withdraw args
  /// </summary>
  public class SyncWithdrawArgs : BaseArgs
  {
    /// <summary>
    /// Max ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public int MaxId { get; set; }
  }
}
