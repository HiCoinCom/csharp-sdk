using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Wallet API implementation for MPC
  /// </summary>
  public class WalletApi : MpcApiBase, IWalletApi
  {
    public WalletApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public WalletResult CreateWallet(string subWalletName, AppShowStatus showStatus)
    {
      var args = new CreateWalletArgs
      {
        SubWalletName = subWalletName,
        ShowStatus = (int)showStatus
      };
      return Invoke<WalletResult>(MpcApiUri.CreateWallet, args);
    }

    /// <inheritdoc/>
    public WalletAddressResult CreateWalletAddress(int subWalletId, string symbol)
    {
      var args = new CreateWalletAddressArgs
      {
        SubWalletId = subWalletId,
        Symbol = symbol
      };
      return Invoke<WalletAddressResult>(MpcApiUri.CreateWalletAddress, args);
    }

    /// <inheritdoc/>
    public WalletAddressListResult GetAddressList(int subWalletId, string symbol, int? maxId = null)
    {
      var args = new QueryAddressArgs
      {
        SubWalletId = subWalletId,
        Symbol = symbol,
        MaxId = maxId
      };
      return Invoke<WalletAddressListResult>(MpcApiUri.GetAddressList, args);
    }

    /// <inheritdoc/>
    public WalletAssetsResult GetWalletAssets(int subWalletId, string? symbol = null)
    {
      var args = new GetWalletAssetsArgs
      {
        SubWalletId = subWalletId,
        Symbol = symbol
      };
      return Invoke<WalletAssetsResult>(MpcApiUri.GetWalletAssets, args);
    }

    /// <inheritdoc/>
    public bool ChangeShowStatus(long[] subWalletIds, int showStatus)
    {
      var args = new ChangeShowStatusArgs
      {
        SubWalletIds = string.Join(",", subWalletIds),
        ShowStatus = showStatus.ToString()
      };
      var result = InvokeRaw(MpcApiUri.ChangeShowStatus, args);
      // 根据 code 判断成功与否
      return result?.Code == 0;
    }

    /// <inheritdoc/>
    public WalletAddressInfoResult GetAddressInfo(string address, string? memo = null)
    {
      var args = new GetAddressInfoArgs
      {
        Address = address,
        Memo = memo
      };
      return Invoke<WalletAddressInfoResult>(MpcApiUri.AddressInfo, args);
    }
  }
}
