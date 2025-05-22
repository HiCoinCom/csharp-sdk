using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class GetCoinDetailsArgs : BaseArgs
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("base_symbol")]
        public string BaseSymbol { get; set; }

        [JsonProperty("open_chain")]
        public bool? OpenChain { get; set; }
    }
}