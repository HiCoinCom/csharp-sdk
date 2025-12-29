using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Account operation related API interface
  /// </summary>
  public interface IAccountApi
  {
    /// <summary>
    /// Get user account by UID and symbol
    /// </summary>
    /// <param name="uid">User ID</param>
    /// <param name="symbol">Coin symbol</param>
    /// <returns>User account result</returns>
    UserAccountResult GetUserAccount(int uid, string symbol);

    /// <summary>
    /// Get user deposit address
    /// </summary>
    /// <param name="uid">User ID</param>
    /// <param name="symbol">Coin symbol</param>
    /// <returns>User address result</returns>
    UserAddressResult GetUserAddress(int uid, string symbol);

    /// <summary>
    /// Sync user address list
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>User address list result</returns>
    UserAddressListResult SyncUserAddressList(int maxId);

    /// <summary>
    /// Get company account by symbol
    /// </summary>
    /// <param name="symbol">Coin symbol</param>
    /// <returns>Account result</returns>
    AccountResult GetCompanyAccount(string symbol);
  }
}
