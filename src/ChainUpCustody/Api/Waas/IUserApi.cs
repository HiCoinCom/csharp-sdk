using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// User management related API interface
  /// </summary>
  public interface IUserApi
  {
    /// <summary>
    /// Register mobile user
    /// </summary>
    /// <param name="country">Country code (e.g., "86" for China)</param>
    /// <param name="mobile">Mobile phone number</param>
    /// <returns>User info result</returns>
    UserInfoResult RegisterMobileUser(string country, string mobile);

    /// <summary>
    /// Register email user
    /// </summary>
    /// <param name="email">Email address</param>
    /// <returns>User info result</returns>
    UserInfoResult RegisterEmailUser(string email);

    /// <summary>
    /// Get mobile user information
    /// </summary>
    /// <param name="country">Country code (e.g., "86" for China)</param>
    /// <param name="mobile">Mobile phone number</param>
    /// <returns>User info result</returns>
    UserInfoResult GetMobileUser(string country, string mobile);

    /// <summary>
    /// Get email user information
    /// </summary>
    /// <param name="email">Email address</param>
    /// <returns>User info result</returns>
    UserInfoResult GetEmailUser(string email);

    /// <summary>
    /// Sync user list
    /// </summary>
    /// <param name="maxId">Maximum ID for pagination</param>
    /// <returns>User info list result</returns>
    UserInfoListResult SyncUserList(int maxId);
  }
}
