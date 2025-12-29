using System;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models
{
  /// <summary>
  /// API response result class
  /// </summary>
  /// <typeparam name="T">Type of the data payload</typeparam>
  public class Result<T>
  {
    /// <summary>
    /// Response code (0 means success)
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// Response message
    /// </summary>
    [JsonProperty("msg")]
    public string Msg { get; set; } = string.Empty;

    /// <summary>
    /// Response data
    /// </summary>
    [JsonProperty("data")]
    public T? Data { get; set; }

    /// <summary>
    /// Check if the request was successful
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Code == 0;

    /// <summary>
    /// Create a success result
    /// </summary>
    public static Result<T> Success(T data)
    {
      return new Result<T>
      {
        Code = 0,
        Msg = "success",
        Data = data
      };
    }

    /// <summary>
    /// Create an error result
    /// </summary>
    public static Result<T> Error(int code, string msg)
    {
      return new Result<T>
      {
        Code = code,
        Msg = msg
      };
    }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  /// <summary>
  /// Non-generic API response result class
  /// </summary>
  public class Result : Result<object>
  {
  }
}
