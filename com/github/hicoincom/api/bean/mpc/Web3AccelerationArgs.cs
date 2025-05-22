using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class Web3AccelerationArgs : BaseArgs
    {
        [JsonProperty("trans_id")]
        public int? TransId { get; set; }

        [JsonProperty("gas_price")]
        public string GasPrice { get; set; }

        [JsonProperty("gas_limit")]
        public string GasLimit { get; set; }
    }
}