using ChainUpCustody.Api.Models.Waas;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Asynchronous callback notification related API interface
  /// </summary>
  public interface IAsyncNotifyApi
  {
    /// <summary>
    /// Parse async notification request
    /// </summary>
    /// <param name="cipher">Encrypted cipher text</param>
    /// <returns>Async notify args</returns>
    AsyncNotifyArgs? NotifyRequest(string cipher);

    /// <summary>
    /// Verify withdraw request
    /// </summary>
    /// <param name="cipher">Encrypted cipher text</param>
    /// <returns>Withdraw args</returns>
    WithdrawArgs? VerifyRequest(string cipher);

    /// <summary>
    /// Generate verify response
    /// </summary>
    /// <param name="withdraw">Withdraw args</param>
    /// <returns>Encrypted response</returns>
    string VerifyResponse(WithdrawArgs withdraw);
  }
}
