using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Workspace API implementation for MPC
  /// </summary>
  public class WorkSpaceApi : MpcApiBase, IWorkSpaceApi
  {
    public WorkSpaceApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public SupportMainChainResult GetSupportMainChain()
    {
      var args = new GetMainChainSymbolArgs();
      return Invoke<SupportMainChainResult>(MpcApiUri.SupportMainChain, args);
    }

    /// <inheritdoc/>
    public CoinDetailsResult GetCoinDetails(string symbol)
    {
      var args = new GetCoinDetailsArgs { Symbol = symbol };
      return Invoke<CoinDetailsResult>(MpcApiUri.CoinDetails, args);
    }

    /// <inheritdoc/>
    public BlockHeightResult GetLastBlockHeight(string baseSymbol)
    {
      var args = new GetLastBlockHeightArgs { BaseSymbol = baseSymbol };
      return Invoke<BlockHeightResult>(MpcApiUri.ChainHeight, args);
    }
  }
}
