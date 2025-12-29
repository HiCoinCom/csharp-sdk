using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Deposit record entity
  /// </summary>
  public class DepositRecord
  {
    /// <summary>
    /// Deposit ID
    /// </summary>
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int? SubWalletId { get; set; }

    /// <summary>
    /// Unique identifier for the coin, used for transfers, e.g.: USDTERC20
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// The unique identifier of the main chain of the coin to be received, e.g.: ETH
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Received coin's contract address
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Receiving amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Transaction source address
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

    /// <summary>
    /// Transaction destination address
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Memo
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Number of block confirmations, e.g.: 10
    /// </summary>
    [JsonProperty("confirmations")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// Transaction block height
    /// </summary>
    [JsonProperty("tx_height")]
    public long? TxHeight { get; set; }

    /// <summary>
    /// Deposit status: 0 waiting, 1 success, 2 fail, 3 pending approval, 4 awaiting approval
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Deposit type: 1 normal deposit, 2 internal transfer deposit
    /// </summary>
    [JsonProperty("deposit_type")]
    public int? DepositType { get; set; }

    /// <summary>
    /// Token ID (for NFT)
    /// </summary>
    [JsonProperty("token_id")]
    public string? TokenId { get; set; }

    /// <summary>
    /// Creation time timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public long? CreatedAt { get; set; }

    /// <summary>
    /// Modification time timestamp
    /// </summary>
    [JsonProperty("updated_at")]
    public long? UpdatedAt { get; set; }

    /// <summary>
    /// Refund amount
    /// </summary>
    [JsonProperty("refund_amount")]
    public decimal? RefundAmount { get; set; }

    /// <summary>
    /// KYT status: "false" not detected, or other values for different status
    /// </summary>
    [JsonProperty("kyt_status")]
    public string? KytStatus { get; set; }
  }
}
