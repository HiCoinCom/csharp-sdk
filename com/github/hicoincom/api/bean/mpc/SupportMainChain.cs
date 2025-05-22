using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class SupportMainChain
    {
        public List<SupportCoin> OpenMainChains { get; set; }

        public List<SupportCoin> SupportMainChains { get; set; }
    }
}