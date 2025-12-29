using System.Collections.Generic;
using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Funding related APIs
  /// </summary>
  public interface IBillingApi
  {
    /// <summary>
    /// Withdraw funds
    /// </summary>
    /// <param name="args">Withdraw args</param>
    /// <returns>Withdraw result</returns>
    WithdrawResult Withdraw(WithdrawArgs args);

    /// <summary>
    /// Get withdrawal list by request IDs
    /// </summary>
    /// <param name="requestIdList">List of request IDs</param>
    /// <returns>Withdraw list result</returns>
    WithdrawListResult WithdrawList(List<string> requestIdList);

    /// <summary>
    /// Sync withdrawal list
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Withdraw list result</returns>
    WithdrawListResult SyncWithdrawList(int maxId);

    /// <summary>
    /// Get deposit list by WaaS IDs
    /// </summary>
    /// <param name="waasIdList">List of WaaS IDs</param>
    /// <returns>Deposit list result</returns>
    DepositListResult DepositList(List<int> waasIdList);

    /// <summary>
    /// Sync deposit list
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Deposit list result</returns>
    DepositListResult SyncDepositList(int maxId);

    /// <summary>
    /// Get miner fee list by WaaS IDs
    /// </summary>
    /// <param name="waasIdList">List of WaaS IDs</param>
    /// <returns>Miner fee list result</returns>
    MinerFeeListResult MinerFeeList(List<int> waasIdList);

    /// <summary>
    /// Sync miner fee list
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Miner fee list result</returns>
    MinerFeeListResult SyncMinerFeeList(int maxId);
  }
}
