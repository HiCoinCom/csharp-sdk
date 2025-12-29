using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// SMS/Email registration parameters
  /// </summary>
  public class RegisterArgs : BaseArgs
  {
    /// <summary>
    /// Country code (e.g., "86" for China)
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
