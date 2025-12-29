using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Web3 acceleration args
  /// </summary>
  public class Web3AccelerationArgs : BaseArgs
  {
    /// <summary>
    /// Web3 transaction ID (required)
    /// </summary>
    [JsonProperty("trans_id")]
    public long TransId { get; set; }

    /// <summary>
    /// Gas price in Gwei (required)
    /// </summary>
    [JsonProperty("gas_price")]
    public string? GasPrice { get; set; }

    /// <summary>
    /// Gas limit (required)
    /// </summary>
    [JsonProperty("gas_limit")]
    public string? GasLimit { get; set; }
  }
}
