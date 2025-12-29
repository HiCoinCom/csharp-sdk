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
  /// Web3 API implementation for MPC
  /// </summary>
  public class Web3Api : MpcApiBase, IWeb3Api
  {
    public Web3Api(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public Web3TransactionResult CreateWeb3Trans(CreateWeb3Args createWeb3Args)
    {
      return CreateWeb3Trans(createWeb3Args, false);
    }

    /// <inheritdoc/>
    public Web3TransactionResult CreateWeb3Trans(CreateWeb3Args createWeb3Args, bool needTransactionSign)
    {
      if (needTransactionSign && !DataCrypto.HasSignKey())
      {
        throw new InvalidOperationException("Configure 'signPrivateKey' as empty");
      }

      if (needTransactionSign)
      {
        var signParamsMap = MpcSignUtil.GetWeb3SignParams(createWeb3Args);
        var signData = MpcSignUtil.ParamsSort(signParamsMap);
        var sign = DataCrypto.Sign(MpcSignUtil.GetMd5(signData));
        createWeb3Args.Sign = sign;
      }

      return Invoke<Web3TransactionResult>(MpcApiUri.CreateWeb3Transaction, createWeb3Args);
    }

    /// <inheritdoc/>
    public bool AccelerationWeb3Trans(Web3AccelerationArgs accelerationArgs)
    {
      var result = Invoke<Web3TransactionResult>(MpcApiUri.Web3TransAcceleration, accelerationArgs);
      return result?.Code == 0;
    }

    /// <inheritdoc/>
    public Web3RecordResult GetWeb3Records(List<string> requestIds)
    {
      var args = new GetWeb3RecordArgs { Ids = string.Join(",", requestIds) };
      return Invoke<Web3RecordResult>(MpcApiUri.Web3TransRecords, args);
    }

    /// <inheritdoc/>
    public Web3RecordResult SyncWeb3Records(int? maxId)
    {
      var args = new SyncWeb3RecordArgs { MaxId = maxId };
      return Invoke<Web3RecordResult>(MpcApiUri.SyncWeb3Records, args);
    }
  }
}
