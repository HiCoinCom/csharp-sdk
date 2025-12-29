using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Get main chain symbol args
  /// </summary>
  public class GetMainChainSymbolArgs : BaseArgs
  {
    /// <summary>
    /// Main chain symbol (Note: API expects "main_chain_symbol" for SupportMainChain)
    /// </summary>
    [JsonProperty("main_chain_symbol")]
    public string? MainChainSymbol { get; set; }
  }

  /// <summary>
  /// Get last block height args (uses base_symbol parameter)
  /// </summary>
  public class GetLastBlockHeightArgs : BaseArgs
  {
    /// <summary>
    /// Base symbol - the main chain symbol (e.g., "ETH", "BTC")
    /// </summary>
    [JsonProperty("base_symbol")]
    public string? BaseSymbol { get; set; }
  }
}
