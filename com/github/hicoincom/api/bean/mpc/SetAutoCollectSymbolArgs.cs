using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class SetAutoCollectSymbolArgs : BaseArgs
    {
        [JsonProperty("collect_min")]
        public string CollectMin { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("fueling_limit")]
        public string FuelingLimit { get; set; }
    }
}