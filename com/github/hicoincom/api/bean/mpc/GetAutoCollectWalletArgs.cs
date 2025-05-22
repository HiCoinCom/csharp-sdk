using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class GetAutoCollectWalletArgs : BaseArgs
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}