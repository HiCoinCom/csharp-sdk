using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Deposit API implementation for MPC
  /// </summary>
  public class DepositApi : MpcApiBase, IDepositApi
  {
    public DepositApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public DepositRecordResult GetDepositRecords(List<int> ids)
    {
      var args = new GetDepositRecordArgs { Ids = string.Join(",", ids) };
      return Invoke<DepositRecordResult>(MpcApiUri.DepositRecords, args);
    }

    /// <inheritdoc/>
    public DepositRecordResult SyncDepositRecords(int? maxId)
    {
      var args = new SyncDepositRecordArgs { MaxId = maxId ?? 0 };
      return Invoke<DepositRecordResult>(MpcApiUri.SyncDepositRecords, args);
    }
  }
}
