using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Withdrawal record
  /// </summary>
  public class WithdrawRecord
  {
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Sub-wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public long? SubWalletId { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Contract address (for token transfers)
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Base chain symbol
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// From address
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

    /// <summary>
    /// To address
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Address memo/tag
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Withdrawal amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Fee token symbol
    /// </summary>
    [JsonProperty("fee_symbol")]
    public string? FeeSymbol { get; set; }

    /// <summary>
    /// Estimated fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Actual fee
    /// </summary>
    [JsonProperty("real_fee")]
    public decimal? RealFee { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Number of confirmations
    /// </summary>
    [JsonProperty("confirmations")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// Transaction block height
    /// </summary>
    [JsonProperty("tx_height")]
    public long? TxHeight { get; set; }

    /// <summary>
    /// Withdrawal source: 1=app, 2=openapi, 3=web
    /// </summary>
    [JsonProperty("withdraw_source")]
    public int? WithdrawSource { get; set; }

    /// <summary>
    /// Delegate fee
    /// </summary>
    [JsonProperty("delegate_fee")]
    public string? DelegateFee { get; set; }

    /// <summary>
    /// Creation time (timestamp in milliseconds)
    /// </summary>
    [JsonProperty("created_at")]
    public long? CreatedAt { get; set; }

    /// <summary>
    /// Update time (timestamp in milliseconds)
    /// </summary>
    [JsonProperty("updated_at")]
    public long? UpdatedAt { get; set; }

    /// <summary>
    /// Output addresses (for multi-output transactions)
    /// </summary>
    [JsonProperty("outputs")]
    public List<WithdrawOutput>? Outputs { get; set; }
  }

  /// <summary>
  /// Withdrawal output address
  /// </summary>
  public class WithdrawOutput
  {
    /// <summary>
    /// Output address
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Output amount
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Address memo/tag
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Whether address needs activation
    /// </summary>
    [JsonProperty("needActive")]
    public bool? NeedActive { get; set; }
  }
}
