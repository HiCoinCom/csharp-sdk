using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class AutoCollectSymbol
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}