using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class WalletAssets
    {
        [JsonProperty("normal_balance")]
        public string NormalBalance { get; set; }

        [JsonProperty("lock_balance")]
        public string LockBalance { get; set; }

        [JsonProperty("collecting_balance")]
        public string CollectingBalance { get; set; }
    }
}