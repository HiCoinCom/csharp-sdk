using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get deposit record args
  /// </summary>
  public class GetDepositRecordArgs : BaseArgs
  {
    /// <summary>
    /// Deposit IDs (comma-separated)
    /// </summary>
    [JsonProperty("ids")]
    public string? Ids { get; set; }
  }
}
