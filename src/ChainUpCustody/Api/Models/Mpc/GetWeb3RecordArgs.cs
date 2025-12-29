using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get Web3 record args
  /// </summary>
  public class GetWeb3RecordArgs : BaseArgs
  {
    /// <summary>
    /// Record IDs (comma-separated)
    /// </summary>
    [JsonProperty("ids")]
    public string? Ids { get; set; }
  }
}
