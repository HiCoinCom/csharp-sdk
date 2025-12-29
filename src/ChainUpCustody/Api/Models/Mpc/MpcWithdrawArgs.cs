using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// MPC Withdraw args
  /// </summary>
  public class MpcWithdrawArgs : BaseArgs
  {
    /// <summary>
    /// Sub wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int? SubWalletId { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// From address
    /// </summary>
    [JsonProperty("from")]
    public string? From { get; set; }

    /// <summary>
    /// To address
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Unique request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// Outputs for batch withdrawal (JSON string)
    /// </summary>
    [JsonProperty("outputs")]
    public string? Outputs { get; set; }

    /// <summary>
    /// Signature
    /// </summary>
    [JsonProperty("sign")]
    public string? Sign { get; set; }
  }
}
