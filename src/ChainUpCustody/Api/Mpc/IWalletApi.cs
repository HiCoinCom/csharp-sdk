using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Enums;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Wallet API interface for MPC
  /// </summary>
  public interface IWalletApi
  {
    /// <summary>
    /// Create a new wallet
    /// </summary>
    /// <param name="subWalletName">Sub wallet name</param>
    /// <param name="showStatus">Show status</param>
    /// <returns>Wallet result</returns>
    WalletResult CreateWallet(string subWalletName, AppShowStatus showStatus);

    /// <summary>
    /// Create wallet address
    /// </summary>
    /// <param name="subWalletId">Sub wallet ID</param>
    /// <param name="symbol">Main chain symbol</param>
    /// <returns>Wallet address result</returns>
    WalletAddressResult CreateWalletAddress(int subWalletId, string symbol);

    /// <summary>
    /// Get address list
    /// </summary>
    /// <param name="subWalletId">Sub wallet ID</param>
    /// <param name="symbol">Main chain symbol</param>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>Wallet address list result</returns>
    WalletAddressListResult GetAddressList(int subWalletId, string symbol, int? maxId = null);

    /// <summary>
    /// Get wallet assets
    /// </summary>
    /// <param name="subWalletId">Sub wallet ID</param>
    /// <param name="symbol">Coin symbol (optional)</param>
    /// <returns>Wallet assets result</returns>
    WalletAssetsResult GetWalletAssets(int subWalletId, string? symbol = null);

    /// <summary>
    /// Change wallet show status
    /// </summary>
    /// <param name="subWalletIds">Array of wallet IDs</param>
    /// <param name="showStatus">Show status (1: show, 2: hidden)</param>
    /// <returns>True if successful (code == 0)</returns>
    bool ChangeShowStatus(long[] subWalletIds, int showStatus);

    /// <summary>
    /// Get address information
    /// </summary>
    /// <param name="address">Wallet address</param>
    /// <param name="memo">Memo (optional)</param>
    /// <returns>Wallet address info result</returns>
    WalletAddressInfoResult GetAddressInfo(string address, string? memo = null);
  }
}
