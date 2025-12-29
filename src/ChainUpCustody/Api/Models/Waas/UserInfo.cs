using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User information entity
  /// </summary>
  public class UserInfo
  {
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// User nickname
    /// </summary>
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }

    /// <summary>
    /// Email address
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Mobile phone number
    /// </summary>
    [JsonProperty("mobile")]
    public string? Mobile { get; set; }

    /// <summary>
    /// Country code
    /// </summary>
    [JsonProperty("country")]
    public string? Country { get; set; }
  }
}
