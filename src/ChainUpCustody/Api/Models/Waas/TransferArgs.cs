using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Transfer args (WaaS internal transfer)
  /// </summary>
  public class TransferArgs : BaseArgs
  {
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Request ID (unique identifier)
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Recipient user ID
    /// </summary>
    [JsonProperty("to")]
    public string? To { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
  }
}
