using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// MPC Withdraw entity
  /// </summary>
  public class MpcWithdraw
  {
    /// <summary>
    /// Withdraw ID
    /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/withdraw_api.rs#L155-L162
    /// </summary>
    [JsonProperty("withdraw_id")]
    public long? WithdrawId { get; set; }
  }
}
