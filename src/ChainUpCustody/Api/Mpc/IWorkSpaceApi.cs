using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Workspace API interface for MPC
  /// </summary>
  public interface IWorkSpaceApi
  {
    /// <summary>
    /// Get supported main chains
    /// </summary>
    /// <returns>Support main chain result</returns>
    SupportMainChainResult GetSupportMainChain();

    /// <summary>
    /// Get coin details
    /// </summary>
    /// <param name="symbol">Coin symbol</param>
    /// <returns>Coin details result</returns>
    CoinDetailsResult GetCoinDetails(string symbol);

    /// <summary>
    /// Get last block height
    /// </summary>
    /// <param name="baseSymbol">Base symbol - the main chain symbol (e.g., "ETH", "BTC")</param>
    /// <returns>Block height result</returns>
    BlockHeightResult GetLastBlockHeight(string baseSymbol);
  }
}
