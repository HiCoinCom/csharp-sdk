using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Withdrawal entity
  /// </summary>
  public class Withdraw
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
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long? Uid { get; set; }

    /// <summary>
    /// User email
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Base symbol (main chain)
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Withdrawal amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Destination address
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Source address
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Transaction ID type (0: on-chain, 1: internal)
    /// </summary>
    [JsonProperty("txid_type")]
    public string? TxidType { get; set; }

    /// <summary>
    /// Number of confirmations
    /// </summary>
    [JsonProperty("confirmations")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// Contract address (for tokens)
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// SaaS status
    /// </summary>
    [JsonProperty("saas_status")]
    public int? SaasStatus { get; set; }

    /// <summary>
    /// Company status
    /// </summary>
    [JsonProperty("company_status")]
    public int? CompanyStatus { get; set; }

    /// <summary>
    /// Withdrawal fee
    /// </summary>
    [JsonProperty("withdraw_fee")]
    public decimal? WithdrawFee { get; set; }

    /// <summary>
    /// Withdrawal fee symbol
    /// </summary>
    [JsonProperty("withdraw_fee_symbol")]
    public string? WithdrawFeeSymbol { get; set; }

    /// <summary>
    /// Platform fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Fee symbol
    /// </summary>
    [JsonProperty("fee_symbol")]
    public string? FeeSymbol { get; set; }

    /// <summary>
    /// Real fee (actual miner fee)
    /// </summary>
    [JsonProperty("real_fee")]
    public decimal? RealFee { get; set; }

    /// <summary>
    /// Creation time (Unix timestamp in milliseconds)
    /// </summary>
    [JsonProperty("created_at")]
    public long? CreatedAt { get; set; }

    /// <summary>
    /// Update time (Unix timestamp in milliseconds)
    /// </summary>
    [JsonProperty("updated_at")]
    public long? UpdatedAt { get; set; }
  }
}
