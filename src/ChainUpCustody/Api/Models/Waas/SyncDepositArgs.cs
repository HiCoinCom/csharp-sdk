using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Sync deposit args
  /// </summary>
  public class SyncDepositArgs : BaseArgs
  {
    /// <summary>
    /// Max ID for pagination
    /// </summary>
    [JsonProperty("max_id")]
    public int MaxId { get; set; }
  }
}
