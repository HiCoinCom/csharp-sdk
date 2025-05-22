using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class AutoCollectWallet
    {
        [JsonProperty("collect_sub_wallet_id")]
        public int? CollectSubWalletId { get; set; }

        [JsonProperty("fueling_sub_wallet_id")]
        public int? FuelingSubWalletId { get; set; }
    }
}