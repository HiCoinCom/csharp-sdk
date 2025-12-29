using System.Collections.Generic;
using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using Microsoft.Extensions.Logging;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Billing API implementation for WaaS
  /// </summary>
  public class BillingApi : WaasApiBase, IBillingApi
  {
    public BillingApi(WaasConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public WithdrawResult Withdraw(WithdrawArgs args)
    {
      if (args == null)
      {
        throw new System.ArgumentNullException(nameof(args), "args cannot be null");
      }
      return Invoke<WithdrawResult>(ApiUri.BillingWithdraw, args);
    }

    /// <inheritdoc/>
    public WithdrawListResult WithdrawList(List<string> requestIdList)
    {
      var args = new StringIdsArgs
      {
        IdList = string.Join(",", requestIdList)
      };
      return Invoke<WithdrawListResult>(ApiUri.WithdrawList, args);
    }

    /// <inheritdoc/>
    public WithdrawListResult SyncWithdrawList(int maxId)
    {
      var args = new SyncWithdrawArgs
      {
        MaxId = maxId
      };
      return Invoke<WithdrawListResult>(ApiUri.SyncWithdraw, args);
    }

    /// <inheritdoc/>
    public DepositListResult DepositList(List<int> waasIdList)
    {
      var args = new StringIdsArgs
      {
        IdList = string.Join(",", waasIdList)
      };
      return Invoke<DepositListResult>(ApiUri.DepositList, args);
    }

    /// <inheritdoc/>
    public DepositListResult SyncDepositList(int maxId)
    {
      var args = new SyncDepositArgs
      {
        MaxId = maxId
      };
      return Invoke<DepositListResult>(ApiUri.SyncDeposit, args);
    }

    /// <inheritdoc/>
    public MinerFeeListResult MinerFeeList(List<int> waasIdList)
    {
      var args = new StringIdsArgs
      {
        IdList = string.Join(",", waasIdList)
      };
      return Invoke<MinerFeeListResult>(ApiUri.MinerFeeList, args);
    }

    /// <inheritdoc/>
    public MinerFeeListResult SyncMinerFeeList(int maxId)
    {
      var args = new SyncMinerFeeArgs
      {
        MaxId = maxId
      };
      return Invoke<MinerFeeListResult>(ApiUri.SyncMinerFee, args);
    }
  }
}
