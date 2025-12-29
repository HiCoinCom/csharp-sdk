using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Coin information entity
  /// </summary>
  public class CoinInfo
  {
    /// <summary>
    /// Coin symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Coin icon URL
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Name of the coin on the blockchain
    /// </summary>
    [JsonProperty("real_symbol")]
    public string? RealSymbol { get; set; }

    /// <summary>
    /// Base symbol (for tokens)
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }

    /// <summary>
    /// Decimal precision
    /// </summary>
    [JsonProperty("decimals")]
    public string? Decimals { get; set; }

    /// <summary>
    /// Contract address (for tokens)
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Deposit confirmation count
    /// </summary>
    [JsonProperty("deposit_confirmation")]
    public int? DepositConfirmation { get; set; }

    /// <summary>
    /// Support memo (0: not supported, 1: supported)
    /// </summary>
    [JsonProperty("support_memo")]
    public byte? SupportMemo { get; set; }

    /// <summary>
    /// Support token (0: not supported, 1: supported)
    /// </summary>
    [JsonProperty("support_token")]
    public byte? SupportToken { get; set; }

    /// <summary>
    /// Address regex pattern
    /// </summary>
    [JsonProperty("address_regex")]
    public string? AddressRegex { get; set; }

    /// <summary>
    /// Address tag regex pattern
    /// </summary>
    [JsonProperty("address_tag_regex")]
    public string? AddressTagRegex { get; set; }

    /// <summary>
    /// Minimum deposit amount
    /// </summary>
    [JsonProperty("min_deposit")]
    public string? MinDeposit { get; set; }

    /// <summary>
    /// Real name of the coin
    /// </summary>
    [JsonProperty("symbol_alias")]
    public string? SymbolAlias { get; set; }

    /// <summary>
    /// Coin network
    /// </summary>
    [JsonProperty("coin_net")]
    public string? CoinNet { get; set; }

    /// <summary>
    /// Withdrawal confirmation count
    /// </summary>
    [JsonProperty("withdraw_confirmation")]
    public int? WithdrawConfirmation { get; set; }

    /// <summary>
    /// Prefix for the block explorer address query link
    /// </summary>
    [JsonProperty("address_link")]
    public string? AddressLink { get; set; }

    /// <summary>
    /// Prefix for the block explorer transaction query link
    /// </summary>
    [JsonProperty("txid_link")]
    public string? TxidLink { get; set; }

    /// <summary>
    /// Merged address main chain coin, Unique identifier for the coin
    /// </summary>
    [JsonProperty("merge_address_symbol")]
    public string? MergeAddressSymbol { get; set; }
  }
}
