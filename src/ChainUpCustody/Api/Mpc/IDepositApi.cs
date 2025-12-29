using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Deposit API interface for MPC
  /// </summary>
  public interface IDepositApi
  {
    /// <summary>
    /// Get deposit records
    /// </summary>
    /// <param name="ids">List of deposit IDs</param>
    /// <returns>Deposit record result</returns>
    DepositRecordResult GetDepositRecords(List<int> ids);

    /// <summary>
    /// Sync deposit records
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Deposit record result</returns>
    DepositRecordResult SyncDepositRecords(int? maxId);
  }
}
