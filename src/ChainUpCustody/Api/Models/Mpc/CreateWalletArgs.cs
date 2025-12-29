using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Create wallet args
  /// </summary>
  public class CreateWalletArgs : BaseArgs
  {
    /// <summary>
    /// Wallet name
    /// </summary>
    [JsonProperty("sub_wallet_name")]
    public string? SubWalletName { get; set; }

    /// <summary>
    /// App show status (1: show, 2: hidden)
    /// </summary>
    [JsonProperty("app_show_status")]
    public int? ShowStatus { get; set; }
  }
}
