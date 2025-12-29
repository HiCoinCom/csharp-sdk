using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Support main chain/coin entity
  /// </summary>
  public class SupportMainChain
  {
    /// <summary>
    /// Unique identifier for the coin, e.g.: ETH
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Coin network, e.g.: Ethereum
    /// </summary>
    [JsonProperty("coin_net")]
    public string? CoinNet { get; set; }

    /// <summary>
    /// Real symbol of the coin
    /// </summary>
    [JsonProperty("real_symbol")]
    public string? RealSymbol { get; set; }

    /// <summary>
    /// Real name of the coin, e.g.: ETH
    /// </summary>
    [JsonProperty("symbol_alias")]
    public string? SymbolAlias { get; set; }

    /// <summary>
    /// Whether memo is supported (0 = no, 1 = yes)
    /// </summary>
    [JsonProperty("is_support_memo")]
    public int? IsSupportMemo { get; set; }

    /// <summary>
    /// Chain ID (e.g., "1" for Ethereum mainnet, "56" for BSC)
    /// </summary>
    [JsonProperty("chain_id")]
    public string? ChainId { get; set; }

    /// <summary>
    /// Whether withdrawal is enabled
    /// </summary>
    [JsonProperty("enable_withdraw")]
    public bool? EnableWithdraw { get; set; }

    /// <summary>
    /// Whether deposit is enabled
    /// </summary>
    [JsonProperty("enable_deposit")]
    public bool? EnableDeposit { get; set; }

    /// <summary>
    /// Indicates if acceleration is supported
    /// </summary>
    [JsonProperty("support_acceleration")]
    public bool? SupportAcceleration { get; set; }

    /// <summary>
    /// Whether payment is required
    /// </summary>
    [JsonProperty("need_payment")]
    public bool? NeedPayment { get; set; }

    /// <summary>
    /// Indicates if the main chain is opened
    /// </summary>
    [JsonProperty("if_open_chain")]
    public bool? IfOpenChain { get; set; }

    /// <summary>
    /// Display order (only in support_main_chain)
    /// </summary>
    [JsonProperty("display_order")]
    public int? DisplayOrder { get; set; }
  }
}
