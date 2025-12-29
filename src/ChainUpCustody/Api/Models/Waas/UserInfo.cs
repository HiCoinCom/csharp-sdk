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
    public long Uid { get; set; }

    /// <summary>
    /// Authentication level
    /// </summary>
    [JsonProperty("auth_level")]
    public int? AuthLevel { get; set; }

    /// <summary>
    /// User nickname
    /// </summary>
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }

    /// <summary>
    /// Real name
    /// </summary>
    [JsonProperty("real_name")]
    public string? RealName { get; set; }

    /// <summary>
    /// Invite code
    /// </summary>
    [JsonProperty("invite_code")]
    public string? InviteCode { get; set; }

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
