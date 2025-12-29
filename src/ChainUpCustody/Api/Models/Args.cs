using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models
{
  /// <summary>
  /// MPC API arguments class with signature support
  /// </summary>
  public class Args : BaseArgs
  {
    /// <summary>
    /// Request signature
    /// </summary>
    [JsonProperty("sign")]
    public string? Sign { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Args()
    {
    }

    /// <summary>
    /// Constructor with appId and data
    /// </summary>
    public Args(string appId, string data)
    {
      AppId = appId;
      Data = data;
      Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    /// <summary>
    /// Create a new instance of Args with current timestamp
    /// </summary>
    public static Args Create(string appId, string data, string sign)
    {
      return new Args
      {
        AppId = appId,
        Data = data,
        Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        Sign = sign
      };
    }

    /// <summary>
    /// Convert to dictionary for HTTP request
    /// </summary>
    public override Dictionary<string, string> ToDictionary()
    {
      var dict = base.ToDictionary();
      if (!string.IsNullOrEmpty(Sign))
      {
        dict.Add("sign", Sign);
      }
      return dict;
    }

    /// <summary>
    /// Convert to Map for HTTP request (alias for ToDictionary)
    /// </summary>
    public Dictionary<string, string> ToMap()
    {
      return ToDictionary();
    }

    /// <summary>
    /// Convert to query string for GET requests
    /// </summary>
    public string ToQueryString()
    {
      var dict = ToDictionary();
      var sb = new StringBuilder();
      foreach (var kvp in dict)
      {
        if (sb.Length > 0)
        {
          sb.Append('&');
        }
        sb.Append(HttpUtility.UrlEncode(kvp.Key));
        sb.Append('=');
        sb.Append(HttpUtility.UrlEncode(kvp.Value));
      }
      return sb.ToString();
    }
  }
}
