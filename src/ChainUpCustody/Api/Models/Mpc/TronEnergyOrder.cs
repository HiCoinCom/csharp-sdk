using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Tron delegate transaction result
  /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/tron_resource_api.rs
  /// </summary>
  public class TronEnergyOrder
  {
    /// <summary>
    /// Transaction ID
    /// </summary>
    [JsonProperty("trans_id")]
    public string? TransId { get; set; }

    /// <summary>
    /// Request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Raw data for additional fields
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? Extra { get; set; }
  }
}
