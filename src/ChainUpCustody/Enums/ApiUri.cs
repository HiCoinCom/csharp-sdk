using System;

namespace ChainUpCustody.Enums
{
  /// <summary>
  /// WaaS API URI enumeration
  /// Contains all API endpoints for the WaaS service
  /// </summary>
  public class ApiUri
  {
    public string Value { get; }
    public string Method { get; }

    private ApiUri(string value, string method)
    {
      Value = value;
      Method = method;
    }

    /// <summary>
    /// User mobile phone registration
    /// </summary>
    public static readonly ApiUri CreateUserMobile = new("/user/createUser", "POST");

    /// <summary>
    /// User email (virtual account) registration
    /// </summary>
    public static readonly ApiUri CreateUserEmail = new("/user/registerEmail", "POST");

    /// <summary>
    /// Query user information
    /// </summary>
    public static readonly ApiUri GetUserInfo = new("/user/info", "POST");

    /// <summary>
    /// Query user list
    /// </summary>
    public static readonly ApiUri GetUserList = new("/user/syncList", "POST");

    /// <summary>
    /// Obtain merchant's coin list
    /// </summary>
    public static readonly ApiUri CoinList = new("/user/getCoinList", "POST");

    /// <summary>
    /// Obtain the user-specified coin account
    /// </summary>
    public static readonly ApiUri UserSymbolAccount = new("/account/getByUidAndSymbol", "POST");

    /// <summary>
    /// Obtain the balance of the merchant account
    /// </summary>
    public static readonly ApiUri CompanySymbolAccount = new("/account/getCompanyBySymbol", "POST");

    /// <summary>
    /// Obtain the address of the user-specified coin account
    /// </summary>
    public static readonly ApiUri UserDepositAddress = new("/account/getDepositAddress", "POST");

    /// <summary>
    /// Deposit address list
    /// </summary>
    public static readonly ApiUri UserDepositAddressList = new("/address/syncList", "POST");

    /// <summary>
    /// Input a specific address and get the response of the corresponding custody user and currency information
    /// </summary>
    public static readonly ApiUri UserDepositAddressInfo = new("/account/getDepositAddressInfo", "POST");

    /// <summary>
    /// Withdrawal operation
    /// </summary>
    public static readonly ApiUri BillingWithdraw = new("/billing/withdraw", "POST");

    /// <summary>
    /// Sync withdrawal record
    /// </summary>
    public static readonly ApiUri SyncWithdraw = new("/billing/syncWithdrawList", "POST");

    /// <summary>
    /// Batch query withdrawal records
    /// </summary>
    public static readonly ApiUri WithdrawList = new("/billing/withdrawList", "POST");

    /// <summary>
    /// Sync deposit record
    /// </summary>
    public static readonly ApiUri SyncDeposit = new("/billing/syncDepositList", "POST");

    /// <summary>
    /// Batch query deposit records
    /// </summary>
    public static readonly ApiUri DepositList = new("/billing/depositList", "POST");

    /// <summary>
    /// Sync gas fee records
    /// </summary>
    public static readonly ApiUri SyncMinerFee = new("/billing/syncMinerFeeList", "POST");

    /// <summary>
    /// Batch query gas fee records
    /// </summary>
    public static readonly ApiUri MinerFeeList = new("/billing/minerFeeList", "POST");

    /// <summary>
    /// WaaS internal merchants transfers
    /// </summary>
    public static readonly ApiUri AccountTransfer = new("/account/transfer", "POST");

    /// <summary>
    /// Query transfer records
    /// </summary>
    public static readonly ApiUri AccountTransferList = new("/account/transferList", "POST");

    /// <summary>
    /// Sync transfer records
    /// </summary>
    public static readonly ApiUri SyncAccountTransferList = new("/account/syncTransferList", "POST");
  }
}
