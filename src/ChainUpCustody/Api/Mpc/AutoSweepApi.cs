using ChainUpCustody.Api.Models;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Auto Sweep API implementation for MPC
  /// </summary>
  public class AutoSweepApi : MpcApiBase, IAutoSweepApi
  {
    public AutoSweepApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public AutoCollectResult AutoCollectSubWallets(string symbol)
    {
      var args = new AutoCollectSubWalletsArgs { Symbol = symbol };
      return Invoke<AutoCollectResult>(MpcApiUri.AutoCollectWallets, args);
    }

    /// <inheritdoc/>
    public bool SetAutoCollectSymbol(string symbol, string collectMin, string fuelingLimit)
    {
      var args = new SetAutoCollectSymbolArgs
      {
        Symbol = symbol,
        CollectMin = collectMin,
        FuelingLimit = fuelingLimit
      };
      var result = Invoke<Result<object>>(MpcApiUri.SetAutoCollectSymbol, args);
      return result != null && result.Code == 0;
    }

    /// <inheritdoc/>
    public AutoSweepRecordResult SyncAutoCollectRecords(long maxId)
    {
      var args = new SyncAutoCollectRecordsArgs { MaxId = maxId };
      return Invoke<AutoSweepRecordResult>(MpcApiUri.SyncAutoSweepRecords, args);
    }
  }
}
