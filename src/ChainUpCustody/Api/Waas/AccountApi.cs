using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Account API implementation for WaaS
  /// </summary>
  public class AccountApi : WaasApiBase, IAccountApi
  {
    public AccountApi(WaasConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public UserAccountResult GetUserAccount(int uid, string symbol)
    {
      var args = new UserAccountArgs
      {
        Uid = uid,
        Symbol = symbol
      };
      return Invoke<UserAccountResult>(ApiUri.UserSymbolAccount, args);
    }

    /// <inheritdoc/>
    public UserAddressResult GetUserAddress(int uid, string symbol)
    {
      var args = new UserAddressArgs
      {
        Uid = uid,
        Symbol = symbol
      };
      return Invoke<UserAddressResult>(ApiUri.UserDepositAddress, args);
    }

    /// <inheritdoc/>
    public UserAddressListResult SyncUserAddressList(int maxId)
    {
      var args = new SyncUserListArgs
      {
        MaxId = maxId
      };
      return Invoke<UserAddressListResult>(ApiUri.UserDepositAddressList, args);
    }

    /// <inheritdoc/>
    public AccountResult GetCompanyAccount(string symbol)
    {
      var args = new CompanyAccountArgs
      {
        Symbol = symbol
      };
      return Invoke<AccountResult>(ApiUri.CompanySymbolAccount, args);
    }
  }
}
