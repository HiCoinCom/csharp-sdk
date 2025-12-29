using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// MPC notify result entity
  /// </summary>
  public class MpcNotify
  {
    /// <summary>
    /// Notify status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }
  }
}
