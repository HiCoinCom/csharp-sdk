using Newtonsoft.Json;
using System;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Miner fee entity
  /// </summary>
  public class MinerFee
  {
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
    /// Symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Fee
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Source address
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

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
    /// Modification time
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Transaction hash
    /// </summary>
    [JsonProperty("txid")]
    public string? Txid { get; set; }

    /// <summary>
    /// Number of block confirmations
    /// </summary>
    [JsonProperty("confirmations")]
    public int? Confirmations { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonProperty("txid_type")]
    public byte? TxidType { get; set; }

    /// <summary>
    /// Base symbol (main chain)
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Contract address
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }
  }
}
