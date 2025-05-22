using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class LastBlockHeight
    {
        [JsonProperty("height")]
        public long? Height { get; set; }
    }
}