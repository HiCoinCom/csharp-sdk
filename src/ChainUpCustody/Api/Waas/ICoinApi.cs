using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Coin related API interface
  /// </summary>
  public interface ICoinApi
  {
    /// <summary>
    /// Get coin list
    /// </summary>
    /// <returns>Coin info list result</returns>
    CoinInfoListResult GetCoinList();
  }
}
