using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// User API implementation for WaaS
  /// </summary>
  public class UserApi : WaasApiBase, IUserApi
  {
    public UserApi(WaasConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public UserInfoResult RegisterEmailUser(string email)
    {
      var args = new RegisterArgs
      {
        Email = email
      };
      return Invoke<UserInfoResult>(ApiUri.CreateUserEmail, args);
    }

    /// <inheritdoc/>
    public UserInfoResult GetEmailUser(string email)
    {
      var args = new UserInfoArgs
      {
        Email = email
      };
      return Invoke<UserInfoResult>(ApiUri.GetUserInfo, args);
    }

    /// <inheritdoc/>
    public UserInfoListResult SyncUserList(int maxId)
    {
      var args = new SyncUserListArgs
      {
        MaxId = maxId
      };
      return Invoke<UserInfoListResult>(ApiUri.GetUserList, args);
    }
  }
}
