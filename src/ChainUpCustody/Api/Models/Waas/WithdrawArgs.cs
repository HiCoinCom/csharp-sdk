using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Withdraw args
  /// </summary>
  public class WithdrawArgs : BaseArgs
  {
    /// <summary>
    /// Request ID (unique identifier)
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// User ID for withdrawal
    /// </summary>
    [JsonProperty("from_uid")]
    public int? FromUid { get; set; }

    /// <summary>
    /// Destination address
    /// </summary>
    [JsonProperty("to_address")]
    public string? ToAddress { get; set; }

    /// <summary>
    /// Withdrawal amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Checksum for verification（create by ChainUp）
    /// </summary>
    [JsonProperty("check_sum")]
    public string? CheckSum { get; set; }
  }
}
