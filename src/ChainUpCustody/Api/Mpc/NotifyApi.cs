using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Mpc
{
  /// <summary>
  /// Notify API implementation for MPC
  /// </summary>
  public class NotifyApi : MpcApiBase, INotifyApi
  {
    public NotifyApi(MpcConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public MpcNotifyArgs? NotifyRequest(string cipher)
    {
      if (string.IsNullOrEmpty(cipher))
      {
        return null;
      }
      var json = DataCrypto.Decode(cipher);
      return JsonConvert.DeserializeObject<MpcNotifyArgs>(json);
    }

    /// <inheritdoc/>
    public MpcNotifyData? DecryptNotification(string encryptedData)
    {
      if (string.IsNullOrEmpty(encryptedData))
      {
        return null;
      }
      var decrypted = DataCrypto.Decode(encryptedData);
      return JsonConvert.DeserializeObject<MpcNotifyData>(decrypted);
    }
  }
}
