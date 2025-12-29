using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Tron Buy Resource API interface for MPC
  /// </summary>
  public interface ITronBuyResourceApi
  {
    /// <summary>
    /// Create Tron delegate (buy energy)
    /// </summary>
    /// <param name="args">Tron buy energy args</param>
    /// <returns>Tron energy order result</returns>
    TronEnergyOrderResult CreateTronDelegate(TronBuyEnergyArgs args);

    /// <summary>
    /// Get buy resource records
    /// </summary>
    /// <param name="requestIds">List of request IDs</param>
    /// <returns>Energy order detail result</returns>
    EnergyOrderDetailResult GetBuyResourceRecords(List<string> requestIds);

    /// <summary>
    /// Sync buy resource records
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Energy order detail result</returns>
    EnergyOrderDetailResult SyncBuyResourceRecords(int? maxId);
  }
}
