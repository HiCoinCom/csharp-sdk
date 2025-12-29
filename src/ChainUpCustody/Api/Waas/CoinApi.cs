using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Coin API implementation for WaaS
  /// </summary>
  public class CoinApi : WaasApiBase, ICoinApi
  {
    public CoinApi(WaasConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public CoinInfoListResult GetCoinList()
    {
      var args = new CoinInfoArgs();
      return Invoke<CoinInfoListResult>(ApiUri.CoinList, args);
    }
  }
}
