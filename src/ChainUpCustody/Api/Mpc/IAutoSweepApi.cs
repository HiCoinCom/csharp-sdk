using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Auto Sweep API interface for MPC
  /// </summary>
  public interface IAutoSweepApi
  {
    /// <summary>
    /// Gets auto-sweep wallets for a specific coin
    /// Retrieve the auto-sweep wallet and auto fueling wallet for a specific coin.
    /// </summary>
    /// <param name="symbol">Unique identifier for the coin (e.g., "USDTERC20")</param>
    /// <returns>Auto collect result containing wallet IDs</returns>
    AutoCollectResult AutoCollectSubWallets(string symbol);

    /// <summary>
    /// Configures auto-sweep for a coin
    /// Set the minimum auto-sweep amount and the maximum miner fee for refueling.
    /// </summary>
    /// <param name="symbol">Unique identifier for the coin</param>
    /// <param name="collectMin">Minimum amount for auto-sweep (up to 6 decimal places)</param>
    /// <param name="fuelingLimit">Maximum miner fee amount for auto-sweep refueling</param>
    /// <returns>True if successful</returns>
    bool SetAutoCollectSymbol(string symbol, string collectMin, string fuelingLimit);

    /// <summary>
    /// Syncs auto-collection records by max ID (pagination)
    /// Retrieve up to 100 sweeping records for all wallets under a workspace.
    /// </summary>
    /// <param name="maxId">Starting ID for sweeping records (default: 0)</param>
    /// <returns>Auto sweep record result</returns>
    AutoSweepRecordResult SyncAutoCollectRecords(long maxId);
  }
}
