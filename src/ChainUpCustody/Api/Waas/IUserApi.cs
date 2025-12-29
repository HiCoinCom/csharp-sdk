using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// User management related API interface
  /// </summary>
  public interface IUserApi
  {
    /// <summary>
    /// Register email user
    /// </summary>
    /// <param name="email">Email address</param>
    /// <returns>User info result</returns>
    UserInfoResult RegisterEmailUser(string email);

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
