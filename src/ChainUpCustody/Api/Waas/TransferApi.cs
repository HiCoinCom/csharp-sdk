using System;
using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Transfer operation related API implementation
  /// </summary>
  public class TransferApi : WaasApiBase, ITransferApi
  {
    public TransferApi(WaasConfig config, IDataCrypto crypto, ILogger? logger = null)
        : base(config, crypto, logger)
    {
    }

    /// <summary>
    /// WaaS internal merchants transfer money to each other
    /// </summary>
    public TransferResult AccountTransfer(TransferArgs args)
    {
      if (args == null)
      {
        throw new ArgumentNullException(nameof(args), "args cannot be null");
      }
      return Invoke<TransferResult>(ApiUri.AccountTransfer, args);
    }

    /// <summary>
    /// Query transfer records
    /// </summary>
    public TransferListResult GetAccountTransferList(string ids, string idsType)
    {
      var args = new TransferListArgs
      {
        Ids = ids,
        IdsType = idsType
      };
      return Invoke<TransferListResult>(ApiUri.AccountTransferList, args);
    }

    /// <summary>
    /// Sync transfer records
    /// </summary>
    public TransferListResult SyncAccountTransferList(int maxId)
    {
      var args = new SyncTransferListArgs
      {
        MaxId = maxId
      };
      return Invoke<TransferListResult>(ApiUri.SyncAccountTransferList, args);
    }
  }
}
