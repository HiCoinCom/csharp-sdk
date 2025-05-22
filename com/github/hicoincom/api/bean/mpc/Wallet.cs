using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class Wallet
    {
        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }
    }
}