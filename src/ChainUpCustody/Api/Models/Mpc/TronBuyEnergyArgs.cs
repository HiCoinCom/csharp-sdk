using Newtonsoft.Json;

namespace ChainUpCustody.Api.Models.Mpc
{
  /// <summary>
  /// Parameters for creating a Tron delegate (Buy Tron Resource)
  /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/tron_resource_api.rs
  /// </summary>
  public class TronBuyEnergyArgs : BaseArgs
  {
    /// <summary>
    /// Unique request ID (required)
    /// </summary>
    [JsonProperty("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Buy type: 0=System, 1=Manual (optional)
    /// </summary>
    [JsonProperty("buy_type")]
    public int? BuyType { get; set; }

    /// <summary>
    /// Resource type: 0=Energy and Bandwidth, 1=Energy only (optional)
    /// </summary>
    [JsonProperty("resource_type")]
    public int? ResourceType { get; set; }

    /// <summary>
    /// Service charge type (required): "10010"=10min, "20001"=1hour, "30001"=1day
    /// </summary>
    [JsonProperty("service_charge_type")]
    public string? ServiceChargeType { get; set; }

    /// <summary>
    /// Energy amount to purchase (optional)
    /// </summary>
    [JsonProperty("energy_num")]
    public long? EnergyNum { get; set; }

    /// <summary>
    /// Bandwidth amount to purchase (optional)
    /// </summary>
    [JsonProperty("net_num")]
    public long? NetNum { get; set; }

    /// <summary>
    /// Address paying for resources (required)
    /// </summary>
    [JsonProperty("address_from")]
    public string? AddressFrom { get; set; }

    /// <summary>
    /// Address to receive resources (optional, required for buy_type 0 or 2)
    /// </summary>
    [JsonProperty("address_to")]
    public string? AddressTo { get; set; }

    /// <summary>
    /// Contract address (optional, required for buy_type 0 or 2)
    /// </summary>
    [JsonProperty("contract_address")]
    public string? ContractAddress { get; set; }
  }
}
