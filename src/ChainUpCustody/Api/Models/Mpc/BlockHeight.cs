using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Block height entity
  /// </summary>
  public class BlockHeight
  {
    /// <summary>
    /// Block height (API may return as "height" or "block_height")
    /// </summary>
    [JsonProperty("height")]
    public long? BlockHeightValue { get; set; }
  }
}
