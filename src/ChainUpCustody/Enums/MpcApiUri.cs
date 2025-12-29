using System;

namespace ChainUpCustody.Enums
{
  /// <summary>
  /// MPC API URI enumeration
  /// Contains all API endpoints for the MPC service
  /// </summary>
  public class MpcApiUri
  {
    public string Value { get; }
    public string Method { get; }

    private MpcApiUri(string value, string method)
    {
      Value = value;
      Method = method;
    }

    /// <summary>
    /// Get Supported Main Chains
    /// </summary>
    public static readonly MpcApiUri SupportMainChain = new("/api/mpc/wallet/open_coin", "GET");

    /// <summary>
    /// Get MPC Workspace Coin Details
    /// </summary>
    public static readonly MpcApiUri CoinDetails = new("/api/mpc/coin_list", "GET");

    /// <summary>
    /// Get Latest Block Height
    /// </summary>
    public static readonly MpcApiUri ChainHeight = new("/api/mpc/chain_height", "GET");

    /// <summary>
    /// Create wallet
    /// </summary>
    public static readonly MpcApiUri CreateWallet = new("/api/mpc/sub_wallet/create", "POST");

    /// <summary>
    /// Create Wallet Address
    /// </summary>
    public static readonly MpcApiUri CreateWalletAddress = new("/api/mpc/sub_wallet/create/address", "POST");

    /// <summary>
    /// Query Wallet Address
    /// </summary>
    public static readonly MpcApiUri GetAddressList = new("/api/mpc/sub_wallet/get/address/list", "POST");

    /// <summary>
    /// Get Wallet Assets
    /// </summary>
    public static readonly MpcApiUri GetWalletAssets = new("/api/mpc/sub_wallet/assets", "GET");

    /// <summary>
    /// Modify the Wallet Display Status
    /// </summary>
    public static readonly MpcApiUri ChangeShowStatus = new("/api/mpc/sub_wallet/change_show_status", "POST");

    /// <summary>
    /// Verify Address Information
    /// </summary>
    public static readonly MpcApiUri AddressInfo = new("/api/mpc/sub_wallet/address/info", "GET");

    /// <summary>
    /// Transfer (Withdrawal)
    /// </summary>
    public static readonly MpcApiUri BillingWithdraw = new("/api/mpc/billing/withdraw", "POST");

    /// <summary>
    /// Get Transfer(withdraw) Records
    /// </summary>
    public static readonly MpcApiUri WithdrawRecords = new("/api/mpc/billing/withdraw_list", "GET");

    /// <summary>
    /// Synchronize Transfer(withdraw) Records
    /// </summary>
    public static readonly MpcApiUri SyncWithdrawRecords = new("/api/mpc/billing/sync_withdraw_list", "GET");

    /// <summary>
    /// Get Transfer(deposit) Records
    /// </summary>
    public static readonly MpcApiUri DepositRecords = new("/api/mpc/billing/deposit_list", "GET");

    /// <summary>
    /// Sync Receiving record
    /// </summary>
    public static readonly MpcApiUri SyncDepositRecords = new("/api/mpc/billing/sync_deposit_list", "GET");

    /// <summary>
    /// Create Web3 Transaction
    /// </summary>
    public static readonly MpcApiUri CreateWeb3Transaction = new("/api/mpc/web3/trans/create", "POST");

    /// <summary>
    /// Web3 Transaction Acceleration
    /// </summary>
    public static readonly MpcApiUri Web3TransAcceleration = new("/api/mpc/web3/pending", "POST");

    /// <summary>
    /// Get Web3 Transaction Records
    /// </summary>
    public static readonly MpcApiUri Web3TransRecords = new("/api/mpc/web3/trans_list", "GET");

    /// <summary>
    /// Sync Web3 Transaction Records
    /// </summary>
    public static readonly MpcApiUri SyncWeb3Records = new("/api/mpc/web3/sync_trans_list", "GET");

    /// <summary>
    /// Get Auto-Sweep Wallets
    /// </summary>
    public static readonly MpcApiUri AutoCollectWallets = new("/api/mpc/auto_collect/sub_wallets", "GET");

    /// <summary>
    /// Configure Auto-Sweep for Coin
    /// </summary>
    public static readonly MpcApiUri SetAutoCollectSymbol = new("/api/mpc/auto_collect/symbol/set", "POST");

    /// <summary>
    /// Sync Auto Sweeping Records
    /// </summary>
    public static readonly MpcApiUri SyncAutoSweepRecords = new("/api/mpc/billing/sync_auto_collect_list", "GET");

    /// <summary>
    /// Create Delegate (Buy Tron Resource)
    /// </summary>
    public static readonly MpcApiUri TronCreateDelegate = new("/api/mpc/tron/delegate", "POST");

    /// <summary>
    /// Buy Tron Resource Records
    /// </summary>
    public static readonly MpcApiUri TronDelegateRecords = new("/api/mpc/tron/delegate/trans_list", "POST");

    /// <summary>
    /// Sync Buy Tron Resource Records
    /// </summary>
    public static readonly MpcApiUri SyncTronDelegateRecords = new("/api/mpc/tron/delegate/sync_trans_list", "POST");
  }
}
