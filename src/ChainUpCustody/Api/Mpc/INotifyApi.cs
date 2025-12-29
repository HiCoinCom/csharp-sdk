using ChainUpCustody.Api.Models.Mpc;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Notify API interface for MPC
  /// </summary>
  public interface INotifyApi
  {
    /// <summary>
    /// Parse notification request (legacy method for backward compatibility)
    /// </summary>
    /// <param name="cipher">Encrypted cipher text</param>
    /// <returns>MPC notify args</returns>
    MpcNotifyArgs? NotifyRequest(string cipher);

    /// <summary>
    /// Decrypts webhook notification data
    /// </summary>
    /// <param name="encryptedData">Encrypted notification data</param>
    /// <returns>Decrypted notification data as MpcNotifyData</returns>
    MpcNotifyData? DecryptNotification(string encryptedData);
  }
}
