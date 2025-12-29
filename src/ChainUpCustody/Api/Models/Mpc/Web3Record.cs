using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Web3 transaction record
  /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/web3_api.rs
  /// </summary>
  public class Web3Record
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
    /// Main chain symbol
    /// </summary>
    [JsonProperty("main_chain_symbol")]
    public string? MainChainSymbol { get; set; }

    /// <summary>
    /// Token symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Interactive contract address
    /// </summary>
    [JsonProperty("interactive_contract")]
    public string? InteractiveContract { get; set; }

    /// <summary>
    /// Transaction amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gas price (Gwei)
    /// </summary>
    [JsonProperty("gas_price")]
    public decimal? GasPrice { get; set; }

    /// <summary>
    /// Gas limit
    /// </summary>
    [JsonProperty("gas_limit")]
    public decimal? GasLimit { get; set; }

    /// <summary>
    /// Gas used
    /// </summary>
    [JsonProperty("gas_used")]
    public decimal? GasUsed { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

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
    /// Fee token symbol
    /// </summary>
    [JsonProperty("fee_symbol")]
    public string? FeeSymbol { get; set; }

    /// <summary>
    /// Transaction status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Transaction type: 0=Authorization, 1=Transaction, 22=TRON permission approve, 23=TRON approved transfer
    /// </summary>
    [JsonProperty("trans_type")]
    public int? TransType { get; set; }

    /// <summary>
    /// Transaction source: 1=web app, 2=open-api
    /// </summary>
    [JsonProperty("trans_source")]
    public int? TransSource { get; set; }

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
    /// Remark
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

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
    /// Input data (hex)
    /// </summary>
    [JsonProperty("input_data")]
    public string? InputData { get; set; }
  }
}
