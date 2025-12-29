using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Withdraw API interface for MPC
  /// </summary>
  public interface IWithdrawApi
  {
    /// <summary>
    /// Transfer (Withdrawal) with transaction sign
    /// </summary>
    /// <param name="withdrawArgs">Withdraw args</param>
    /// <param name="needTransactionSign">Whether transactions require signature</param>
    /// <returns>Withdraw result</returns>
    MpcWithdrawResult Withdraw(MpcWithdrawArgs withdrawArgs, bool needTransactionSign);

    /// <summary>
    /// Transfer (Withdrawal)
    /// </summary>
    /// <param name="withdrawArgs">Withdraw args</param>
    /// <returns>Withdraw result</returns>
    MpcWithdrawResult Withdraw(MpcWithdrawArgs withdrawArgs);

    /// <summary>
    /// Get withdraw records
    /// </summary>
    /// <param name="requestIds">List of request IDs</param>
    /// <returns>Withdraw record result</returns>
    WithdrawRecordResult GetWithdrawRecords(List<string> requestIds);

    /// <summary>
    /// Sync withdraw records
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Withdraw record result</returns>
    WithdrawRecordResult SyncWithdrawRecords(int? maxId);
  }
}
