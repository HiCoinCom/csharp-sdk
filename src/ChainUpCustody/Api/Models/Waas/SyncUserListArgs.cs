using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Sync user list args
  /// </summary>
  public class SyncUserListArgs : BaseArgs
  {
    /// <summary>
    /// Max ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public int? MaxId { get; set; }
  }
}
