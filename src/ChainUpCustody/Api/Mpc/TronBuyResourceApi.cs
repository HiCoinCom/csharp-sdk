using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Tron Buy Resource API implementation for MPC
  /// </summary>
  public class TronBuyResourceApi : MpcApiBase, ITronBuyResourceApi
  {
    public TronBuyResourceApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public TronEnergyOrderResult CreateTronDelegate(TronBuyEnergyArgs args)
    {
      return Invoke<TronEnergyOrderResult>(MpcApiUri.TronCreateDelegate, args);
    }

    /// <inheritdoc/>
    public EnergyOrderDetailResult GetBuyResourceRecords(List<string> requestIds)
    {
      var args = new QueryEnergyOrderArgs { Ids = string.Join(",", requestIds) };
      return Invoke<EnergyOrderDetailResult>(MpcApiUri.TronDelegateRecords, args);
    }

    /// <inheritdoc/>
    public EnergyOrderDetailResult SyncBuyResourceRecords(int? maxId)
    {
      var args = new SyncAutoSweepRecordArgs { MaxId = maxId };
      return Invoke<EnergyOrderDetailResult>(MpcApiUri.SyncTronDelegateRecords, args);
    }
  }
}
