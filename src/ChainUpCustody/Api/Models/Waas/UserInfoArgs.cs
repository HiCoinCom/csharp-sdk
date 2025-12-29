using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User info query args
  /// </summary>
  public class UserInfoArgs : BaseArgs
  {
    /// <summary>
    /// Country code
    /// </summary>
    [JsonProperty("country")]
    public string? Country { get; set; }

    /// <summary>
    /// Mobile phone number
    /// </summary>
    [JsonProperty("mobile")]
    public string? Mobile { get; set; }

    /// <summary>
    /// Email address
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }
  }
}
