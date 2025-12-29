using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Web3 API interface for MPC
  /// </summary>
  public interface IWeb3Api
  {
    /// <summary>
    /// Create Web3 transaction with transaction sign
    /// </summary>
    /// <param name="web3TransArgs">Web3 transaction args</param>
    /// <param name="needTransactionSign">Whether transactions require signature</param>
    /// <returns>Web3 transaction result</returns>
    Web3TransactionResult CreateWeb3Trans(CreateWeb3Args web3TransArgs, bool needTransactionSign);

    /// <summary>
    /// Create Web3 transaction
    /// </summary>
    /// <param name="web3TransArgs">Web3 transaction args</param>
    /// <returns>Web3 transaction result</returns>
    Web3TransactionResult CreateWeb3Trans(CreateWeb3Args web3TransArgs);

    /// <summary>
    /// Web3 transaction acceleration
    /// </summary>
    /// <param name="accelerationArgs">Acceleration args</param>
    /// <returns>True if successful</returns>
    bool AccelerationWeb3Trans(Web3AccelerationArgs accelerationArgs);

    /// <summary>
    /// Get Web3 records
    /// </summary>
    /// <param name="requestIds">List of request IDs</param>
    /// <returns>Web3 record result</returns>
    Web3RecordResult GetWeb3Records(List<string> requestIds);

    /// <summary>
    /// Sync Web3 records
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Web3 record result</returns>
    Web3RecordResult SyncWeb3Records(int? maxId);
  }
}
