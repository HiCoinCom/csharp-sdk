using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Transfer operation related API interface
  /// 
  /// This interface provides methods for internal transfers between
  /// merchant users within the WaaS platform.
  /// </summary>
  public interface ITransferApi
  {
    /// <summary>
    /// Query type constant: by request ID
    /// </summary>
    const string REQUEST_ID = "request_id";

    /// <summary>
    /// Query type constant: by receipt
    /// </summary>
    const string RECEIPT = "receipt";

    /// <summary>
    /// Execute internal transfer between WaaS merchant users
    /// 
    /// Transfers funds from one user account to another within the same merchant.
    /// </summary>
    /// <param name="args">TransferArgs containing transfer details (amount, from/to users, etc.)</param>
    /// <returns>TransferResult containing transfer operation result</returns>
    TransferResult AccountTransfer(TransferArgs args);

    /// <summary>
    /// Query transfer records by IDs
    /// 
    /// Retrieve transfer records using either request IDs or receipts.
    /// </summary>
    /// <param name="ids">Comma-separated list of IDs to query</param>
    /// <param name="idsType">Type of IDs: REQUEST_ID or RECEIPT</param>
    /// <returns>TransferListResult containing list of transfer records</returns>
    TransferListResult GetAccountTransferList(string ids, string idsType);

    /// <summary>
    /// Synchronize transfer records
    /// 
    /// Retrieves transfer records with ID greater than the specified maxId,
    /// useful for incremental synchronization.
    /// </summary>
    /// <param name="maxId">Maximum ID from previous sync (0 for first sync)</param>
    /// <returns>TransferListResult containing list of new transfer records</returns>
    TransferListResult SyncAccountTransferList(int maxId);
  }
}
