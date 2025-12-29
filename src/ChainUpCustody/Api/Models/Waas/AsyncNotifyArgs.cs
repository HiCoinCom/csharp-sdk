using System;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Async notify args
  /// </summary>
  public class AsyncNotifyArgs : BaseArgs
  {
    /// <summary>
    /// Side: deposit or withdraw
    /// </summary>
    [JsonProperty("side")]
    public string? Side { get; set; }

    /// <summary>
    /// Notification time
    /// </summary>
    [JsonProperty("notify_time")]
    public DateTime? NotifyTime { get; set; }

    /// <summary>
    /// Request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public string? Uid { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Withdrawal fee symbol
    /// </summary>
    [JsonProperty("withdraw_fee_symbol")]
    public string? WithdrawFeeSymbol { get; set; }

    /// <summary>
    /// Withdrawal fee
    /// </summary>
    [JsonProperty("withdraw_fee")]
    public decimal WithdrawFee { get; set; }

    /// <summary>
    /// Fee symbol
    /// </summary>
    [JsonProperty("fee_symbol")]
    public string? FeeSymbol { get; set; }

    /// <summary>
    /// Real fee
    /// </summary>
    [JsonProperty("real_fee")]
    public decimal RealFee { get; set; }

    /// <summary>
    /// Destination address
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Number of confirmations
    /// </summary>
    [JsonProperty("confirmations")]
    public int Confirmations { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }
  }
}
