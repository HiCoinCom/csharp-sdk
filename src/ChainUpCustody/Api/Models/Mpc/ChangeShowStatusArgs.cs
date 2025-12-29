using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Change wallet show status args
  /// </summary>
  public class ChangeShowStatusArgs : BaseArgs
  {
    /// <summary>
    /// Comma-separated wallet IDs
    /// </summary>
    [JsonProperty("sub_wallet_ids")]
    public string? SubWalletIds { get; set; }

    /// <summary>
    /// Show status (1: show, 2: hidden)
    /// </summary>
    [JsonProperty("app_show_status")]
    public string? ShowStatus { get; set; }
  }
}
