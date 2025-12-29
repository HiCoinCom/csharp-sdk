using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Tron resource record (buy resource record)
  /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/tron_resource_api.rs
  /// </summary>
  public class EnergyOrderDetail
  {
    /// <summary>
    /// Record ID
    /// </summary>
    [JsonProperty("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Request ID
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Buy type: 0=System estimate, 1=Customer specified quantity
    /// </summary>
    [JsonProperty("buy_type")]
    public int? BuyType { get; set; }

    /// <summary>
    /// Resource type: 0=Energy and Bandwidth, 1=Energy only
    /// </summary>
    [JsonProperty("resource_type")]
    public int? ResourceType { get; set; }

    /// <summary>
    /// Service charge rate
    /// </summary>
    [JsonProperty("service_charge_rate")]
    public string? ServiceChargeRate { get; set; }

    /// <summary>
    /// Service charge amount
    /// </summary>
    [JsonProperty("service_charge")]
    public string? ServiceCharge { get; set; }

    /// <summary>
    /// Energy amount
    /// </summary>
    [JsonProperty("energy_num")]
    public long? EnergyNum { get; set; }

    /// <summary>
    /// Bandwidth amount
    /// </summary>
    [JsonProperty("net_num")]
    public long? NetNum { get; set; }

    /// <summary>
    /// From address
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

    /// <summary>
    /// To address
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Contract address
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Energy transaction hash
    /// </summary>
    [JsonProperty("energy_txid")]
    public string? EnergyTxid { get; set; }

    /// <summary>
    /// Net/Bandwidth transaction hash
    /// </summary>
    [JsonProperty("net_txid")]
    public string? NetTxid { get; set; }

    /// <summary>
    /// Reclaim energy transaction hash
    /// </summary>
    [JsonProperty("reclaim_energy_txid")]
    public string? ReclaimEnergyTxid { get; set; }

    /// <summary>
    /// Reclaim net transaction hash
    /// </summary>
    [JsonProperty("reclaim_net_txid")]
    public string? ReclaimNetTxid { get; set; }

    /// <summary>
    /// Energy delegation time (timestamp in ms)
    /// </summary>
    [JsonProperty("energy_time")]
    public long? EnergyTime { get; set; }

    /// <summary>
    /// Net delegation time (timestamp in ms)
    /// </summary>
    [JsonProperty("net_time")]
    public long? NetTime { get; set; }

    /// <summary>
    /// Energy reclaim time (timestamp in ms)
    /// </summary>
    [JsonProperty("reclaim_energy_time")]
    public long? ReclaimEnergyTime { get; set; }

    /// <summary>
    /// Net reclaim time (timestamp in ms)
    /// </summary>
    [JsonProperty("reclaim_net_time")]
    public long? ReclaimNetTime { get; set; }

    /// <summary>
    /// Energy price
    /// </summary>
    [JsonProperty("energy_price")]
    public decimal? EnergyPrice { get; set; }

    /// <summary>
    /// Net/Bandwidth price
    /// </summary>
    [JsonProperty("net_price")]
    public decimal? NetPrice { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public int? Status { get; set; }
  }
}
