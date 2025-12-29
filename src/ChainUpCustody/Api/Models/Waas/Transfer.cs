using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Waas
{
  /// <summary>
  /// Transfer entity
  /// </summary>
  public class Transfer
  {
    /// <summary>
    /// Transfer ID
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Transfer amount
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Sender
    /// </summary>
    [JsonProperty("from")]
    public string? From { get; set; }

    /// <summary>
    /// Recipient
    /// </summary>
    [JsonProperty("to")]
    public string? To { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public long? CreatedAt { get; set; }

    /// <summary>
    /// Request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Receipt
    /// </summary>
    [JsonProperty("receipt")]
    public string? Receipt { get; set; }

    /// <summary>
    /// Remark
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }
  }
}
