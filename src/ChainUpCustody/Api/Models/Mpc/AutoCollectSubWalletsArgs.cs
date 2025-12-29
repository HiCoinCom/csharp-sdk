using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Parameters for getting auto-collect wallets
  /// </summary>
  public class AutoCollectSubWalletsArgs : BaseArgs
  {
    /// <summary>
    /// Unique identifier for the coin (e.g., "USDTERC20")
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }
  }
}
