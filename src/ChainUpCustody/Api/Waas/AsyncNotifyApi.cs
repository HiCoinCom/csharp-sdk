using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Waas
{
  /// <summary>
  /// Async notification API implementation for WaaS
  /// </summary>
  public class AsyncNotifyApi : WaasApiBase, IAsyncNotifyApi
  {
    public AsyncNotifyApi(WaasConfig config, IDataCrypto dataCrypto, ILogger? logger = null)
        : base(config, dataCrypto, logger)
    {
    }

    /// <inheritdoc/>
    public AsyncNotifyArgs? NotifyRequest(string cipher)
    {
      if (string.IsNullOrEmpty(cipher))
      {
        return null;
      }
      var json = DataCrypto.Decode(cipher);
      return JsonConvert.DeserializeObject<AsyncNotifyArgs>(json);
    }

    /// <inheritdoc/>
    public WithdrawArgs? VerifyRequest(string cipher)
    {
      if (string.IsNullOrEmpty(cipher))
      {
        return null;
      }
      var json = DataCrypto.Decode(cipher);
      Console.WriteLine($"[DEBUG] VerifyRequest Raw JSON: {json}");
      return JsonConvert.DeserializeObject<WithdrawArgs>(json);
    }

    /// <inheritdoc/>
    public string VerifyResponse(WithdrawArgs withdraw)
    {
      if (withdraw == null)
      {
        return string.Empty;
      }
      var json = JsonConvert.SerializeObject(withdraw);
      return DataCrypto.Encode(json);
    }
  }
}
