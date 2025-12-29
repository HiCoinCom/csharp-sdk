using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get withdraw record args
  /// </summary>
  public class GetWithdrawRecordArgs : BaseArgs
  {
    /// <summary>
    /// Withdraw IDs (comma-separated)
    /// </summary>
    [JsonProperty("ids")]
    public string? Ids { get; set; }
  }
}
