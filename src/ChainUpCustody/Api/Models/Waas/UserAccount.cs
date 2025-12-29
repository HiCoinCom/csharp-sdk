using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// User account entity
  /// </summary>
  public class UserAccount
  {
    /// <summary>
    /// Normal balance
    /// </summary>
    [JsonProperty("normal_balance")]
    public decimal NormalBalance { get; set; }

    /// <summary>
    /// Locked balance
    /// </summary>
    [JsonProperty("lock_balance")]
    public decimal LockBalance { get; set; }

    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("deposit_address")]
    public string? DepositAddress { get; set; }
  }
}
