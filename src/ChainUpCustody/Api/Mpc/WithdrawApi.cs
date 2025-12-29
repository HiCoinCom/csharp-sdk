using System;
using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using ChainUpCustody.Utilities;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Withdraw API implementation for MPC
  /// </summary>
  public class WithdrawApi : MpcApiBase, IWithdrawApi
  {
    public WithdrawApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public MpcWithdrawResult Withdraw(MpcWithdrawArgs withdrawArgs)
    {
      return Withdraw(withdrawArgs, false);
    }

    /// <inheritdoc/>
    public MpcWithdrawResult Withdraw(MpcWithdrawArgs withdrawArgs, bool needTransactionSign)
    {
      if (needTransactionSign && !DataCrypto.HasSignKey())
      {
        throw new InvalidOperationException("Configure 'signPrivateKey' as empty");
      }

      if (needTransactionSign)
      {
        var signParamsMap = MpcSignUtil.GetWithdrawSignParams(withdrawArgs);
        var signData = MpcSignUtil.ParamsSort(signParamsMap);
        var sign = DataCrypto.Sign(MpcSignUtil.GetMd5(signData));
        withdrawArgs.Sign = sign;
      }

      return Invoke<MpcWithdrawResult>(MpcApiUri.BillingWithdraw, withdrawArgs);
    }

    /// <inheritdoc/>
    public WithdrawRecordResult GetWithdrawRecords(List<string> requestIds)
    {
      var args = new GetWithdrawRecordArgs { Ids = string.Join(",", requestIds) };
      return Invoke<WithdrawRecordResult>(MpcApiUri.WithdrawRecords, args);
    }

    /// <inheritdoc/>
    public WithdrawRecordResult SyncWithdrawRecords(int? maxId)
    {
      var args = new SyncWithdrawRecordArgs { MaxId = maxId };
      return Invoke<WithdrawRecordResult>(MpcApiUri.SyncWithdrawRecords, args);
    }
  }
}
