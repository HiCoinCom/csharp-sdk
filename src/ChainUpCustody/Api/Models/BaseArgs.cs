using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models
{
  /// <summary>
  /// Base API arguments class
  /// </summary>
  public class BaseArgs
  {
    /// <summary>
    /// Application ID
    /// </summary>
    [JsonProperty("app_id")]
    public string? AppId { get; set; }

    /// <summary>
    /// Encrypted data
    /// </summary>
    [JsonProperty("data")]
    public string? Data { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("time")]
    public long Time { get; set; }

    /// <summary>
    /// Character encoding
    /// </summary>
    [JsonProperty("charset")]
    public string? Charset { get; set; }

    /// <summary>
    /// Create a new instance of BaseArgs with current timestamp
    /// </summary>
    public static BaseArgs Create(string appId, string data)
    {
      return new BaseArgs
      {
        AppId = appId,
        Data = data,
        Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
      };
    }

    /// <summary>
    /// Convert to dictionary for HTTP request
    /// </summary>
    public virtual Dictionary<string, string> ToDictionary()
    {
      return new Dictionary<string, string>
            {
                { "app_id", AppId ?? string.Empty },
                { "data", Data ?? string.Empty },
                { "time", Time.ToString() }
            };
    }

    /// <summary>
    /// Serialize to JSON string
    /// </summary>
    public virtual string ToJson()
    {
      return JsonConvert.SerializeObject(this, new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore
      });
    }
  }
}
