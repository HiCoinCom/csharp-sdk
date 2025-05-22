using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class GetLastBlockHeightArgs : BaseArgs
    {
        [JsonProperty("base_symbol")]
        public string BaseSymbol { get; set; }
    }
}