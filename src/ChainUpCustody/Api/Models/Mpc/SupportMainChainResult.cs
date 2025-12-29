using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Response for get supported coins API
  /// Contains both opened and supported main chains
  /// </summary>
  public class SupportedCoinsData
  {
    /// <summary>
    /// List of opened main chains
    /// </summary>
    [JsonProperty("open_main_chain")]
    public List<SupportMainChain>? OpenMainChain { get; set; }

    /// <summary>
    /// List of supported main chains
    /// </summary>
    [JsonProperty("support_main_chain")]
    public List<SupportMainChain>? SupportMainChainList { get; set; }
  }

  /// <summary>
  /// Support main chain result
  /// </summary>
  public class SupportMainChainResult : Result<SupportedCoinsData>
  {
  }
}
