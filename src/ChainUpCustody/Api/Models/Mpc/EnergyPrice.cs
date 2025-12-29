using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Energy price entity
  /// </summary>
  public class EnergyPrice
  {
    /// <summary>
    /// Price per energy
    /// </summary>
    [JsonProperty("price")]
    public string? Price { get; set; }
  }
}
