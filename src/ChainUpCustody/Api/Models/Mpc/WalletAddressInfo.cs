using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Wallet address info entity
  /// </summary>
  public class WalletAddressInfo
  {
    /// <summary>
    /// Wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int? SubWalletId { get; set; }

    /// <summary>
    /// Address type, 1: User address, 2: System address (including collection address, change address).
    /// System address cannot be assigned to users, change from UTXO transactions will all go to the change address
    /// </summary>
    [JsonProperty("addr_type")]
    public int? AddrType { get; set; }

    /// <summary>
    /// Unique coin identifier for merged address
    /// </summary>
    [JsonProperty("merge_address_symbol")]
    public string? MergeAddressSymbol { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Memo/Tag
    /// </summary>
    [JsonProperty("memo")]
    public string? Memo { get; set; }
  }
}
