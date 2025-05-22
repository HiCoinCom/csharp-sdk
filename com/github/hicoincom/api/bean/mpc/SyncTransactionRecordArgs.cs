using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class SyncTransactionRecordArgs : BaseArgs
    {
        [JsonProperty("max_id")]
        public int? MaxId { get; set; }
    }
}