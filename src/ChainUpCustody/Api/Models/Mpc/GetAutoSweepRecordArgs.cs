using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get auto sweep record args
  /// </summary>
  public class GetAutoSweepRecordArgs : BaseArgs
  {
    /// <summary>
    /// Record IDs (comma-separated)
    /// </summary>
    [JsonProperty("ids")]
    public string? Ids { get; set; }
  }
}
