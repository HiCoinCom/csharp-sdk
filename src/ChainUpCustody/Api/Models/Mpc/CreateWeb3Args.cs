using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Create Web3 transaction args
  /// </summary>
  public class CreateWeb3Args : BaseArgs
  {
    /// <summary>
    /// Sub wallet ID
    /// </summary>
    [JsonProperty("sub_wallet_id")]
    public int? SubWalletId { get; set; }

    /// <summary>
    /// Unique request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Main chain symbol
    /// </summary>
    [JsonProperty("main_chain_symbol")]
    public string? MainChainSymbol { get; set; }

    /// <summary>
    /// From address
    /// </summary>
    [JsonProperty("from")]
    public string? From { get; set; }

    /// <summary>
    /// Interactive contract address
    /// </summary>
    [JsonProperty("interactive_contract")]
    public string? InteractiveContract { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Gas price
    /// </summary>
    [JsonProperty("gas_price")]
    public string? GasPrice { get; set; }

    /// <summary>
    /// Gas limit
    /// </summary>
    [JsonProperty("gas_limit")]
    public string? GasLimit { get; set; }

    /// <summary>
    /// Input data (hex)
    /// </summary>
    [JsonProperty("input_data")]
    public string? InputData { get; set; }

    /// <summary>
    /// Transaction type (1: transfer, 2: contract interaction)
    /// </summary>
    [JsonProperty("trans_type")]
    public int? TransType { get; set; }

    /// <summary>
    /// DApp name
    /// </summary>
    [JsonProperty("dapp_name")]
    public string? DappName { get; set; }

    /// <summary>
    /// DApp URL
    /// </summary>
    [JsonProperty("dapp_url")]
    public string? DappUrl { get; set; }

    /// <summary>
    /// DApp image URL
    /// </summary>
    [JsonProperty("dapp_img")]
    public string? DappImg { get; set; }

    /// <summary>
    /// Signature
    /// </summary>
    [JsonProperty("sign")]
    public string? Sign { get; set; }
  }
}
