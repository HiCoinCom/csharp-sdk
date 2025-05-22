using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class GetTransactionRecordArgs : BaseArgs
    {
        [JsonProperty("ids")]
        public string Ids { get; set; }
    }
}