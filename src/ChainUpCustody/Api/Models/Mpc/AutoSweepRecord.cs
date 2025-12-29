using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Auto sweep/collect record entity
  /// </summary>
  public class AutoSweepRecord
  {
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public long? SubWalletId { get; set; }

    /// <summary>
    /// Unique identifier for the coin, e.g.: USDTERC20
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// The unique identifier of the main chain of the coin, e.g.: ETH
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Contract address
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Amount
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
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Address memo/tag
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// Fee currency, e.g.: ETH
    /// </summary>
    [JsonProperty("fee_symbol")]
    public string? FeeSymbol { get; set; }

    /// <summary>
    /// Miner fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Actual fee consumed
    /// </summary>
    [JsonProperty("real_fee")]
    public decimal? RealFee { get; set; }

    /// <summary>
    /// Number of block confirmations
    /// </summary>
    [JsonProperty("confirmations")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// Transaction block height
    /// </summary>
    [JsonProperty("tx_height")]
    public long? TxHeight { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Transaction type: 1 sweep, 2 fueling
    /// </summary>
    [JsonProperty("trans_type")]
    public int? TransType { get; set; }

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
  }
}
