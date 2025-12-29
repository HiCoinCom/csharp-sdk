using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// MPC notification data structure
  /// </summary>
  public class MpcNotifyData : BaseArgs
  {
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Notification side (deposit/withdraw)
    /// </summary>
    [JsonProperty("side")]
    public string? Side { get; set; }

    /// <summary>
    /// Notification type
    /// </summary>
    [JsonProperty("notify_type")]
    public string? NotifyType { get; set; }

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
    /// App ID
    /// </summary>
    [JsonProperty("app_id")]
    public new string? AppId { get; set; }

    /// <summary>
    /// Main chain symbol / Base symbol
    /// </summary>
    [JsonProperty("main_chain_symbol")]
    public string? MainChainSymbol { get; set; }

    /// <summary>
    /// Base symbol
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Contract address (for tokens)
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public string? Fee { get; set; }

    /// <summary>
    /// Real fee
    /// </summary>
    [JsonProperty("real_fee")]
    public string? RealFee { get; set; }

    /// <summary>
    /// Fee symbol
    /// </summary>
    [JsonProperty("fee_symbol")]
    public string? FeeSymbol { get; set; }

    /// <summary>
    /// Refund amount
    /// </summary>
    [JsonProperty("refund_amount")]
    public string? RefundAmount { get; set; }

    /// <summary>
    /// TRON delegate fee
    /// </summary>
    [JsonProperty("delegate_fee")]
    public string? DelegateFee { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Transaction height
    /// </summary>
    [JsonProperty("tx_height")]
    public long? TxHeight { get; set; }

    /// <summary>
    /// Block height
    /// </summary>
    [JsonProperty("block_height")]
    public long? BlockHeight { get; set; }

    /// <summary>
    /// Block time
    /// </summary>
    [JsonProperty("block_time")]
    public long? BlockTime { get; set; }

    /// <summary>
    /// Confirmations count
    /// </summary>
    [JsonProperty("confirmations")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// From address
    /// </summary>
    [JsonProperty("from")]
    public string? From { get; set; }

    /// <summary>
    /// To address
    /// </summary>
    [JsonProperty("to")]
    public string? To { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Address from
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

    /// <summary>
    /// Address to
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Confirmation count (alias)
    /// </summary>
    [JsonProperty("confirm")]
    public int? Confirm { get; set; }

    /// <summary>
    /// Safe confirmation count
    /// </summary>
    [JsonProperty("safe_confirm")]
    public int? SafeConfirm { get; set; }

    /// <summary>
    /// Is mining reward
    /// </summary>
    [JsonProperty("is_mining")]
    public int? IsMining { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonProperty("trans_type")]
    public int? TransType { get; set; }

    /// <summary>
    /// Withdraw source
    /// </summary>
    [JsonProperty("withdraw_source")]
    public string? WithdrawSource { get; set; }

    /// <summary>
    /// KYT status
    /// </summary>
    [JsonProperty("kyt_status")]
    public bool? KytStatus { get; set; }

    /// <summary>
    /// Interactive contract address (Web3)
    /// </summary>
    [JsonProperty("interactive_contract")]
    public string? InteractiveContract { get; set; }

    /// <summary>
    /// Input data (Web3)
    /// </summary>
    [JsonProperty("input_data")]
    public string? InputData { get; set; }

    /// <summary>
    /// Dapp image URL (Web3)
    /// </summary>
    [JsonProperty("dapp_img")]
    public string? DappImg { get; set; }

    /// <summary>
    /// Dapp name (Web3)
    /// </summary>
    [JsonProperty("dapp_name")]
    public string? DappName { get; set; }

    /// <summary>
    /// Dapp URL (Web3)
    /// </summary>
    [JsonProperty("dapp_url")]
    public string? DappUrl { get; set; }

    /// <summary>
    /// Charset
    /// </summary>
    [JsonProperty("charset")]
    public new string? Charset { get; set; }

    /// <summary>
    /// Sign
    /// </summary>
    [JsonProperty("sign")]
    public string? Sign { get; set; }

    /// <summary>
    /// Notify time
    /// </summary>
    [JsonProperty("notify_time")]
    public string? NotifyTime { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("updated_at")]
    public string? UpdatedAt { get; set; }
  }

  /// <summary>
  /// MPC notify args (legacy alias for backward compatibility)
  /// </summary>
  public class MpcNotifyArgs : MpcNotifyData
  {
  }
}
