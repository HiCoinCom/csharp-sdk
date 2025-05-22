using System.Text;
using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;

namespace com.github.hicoincom.util
{
    public class MpcSignUtil
    {
        public static Dictionary<string, string> getWithdrawSignParams(WithdrawArgs withdrawArgs)
        {
            Dictionary<string, string> signParamsMap = new Dictionary<string, string>(6);
            signParamsMap.Add("request_id", withdrawArgs.RequestId);
            signParamsMap.Add("sub_wallet_id", withdrawArgs.SubWalletId?.ToString() ?? "");
            signParamsMap.Add("symbol", withdrawArgs.Symbol);
            signParamsMap.Add("Address_to", withdrawArgs.AddressTo);
            signParamsMap.Add("amount", withdrawArgs.Amount);
            signParamsMap.Add("memo", withdrawArgs.Memo);
            return signParamsMap;
        }

        public static Dictionary<string, string> getWeb3SignParams(CreateWeb3Args createWeb3Args)
        {
            Dictionary<string, string> signParamsMap = new Dictionary<string, string>(6);
            signParamsMap.Add("request_id", createWeb3Args.RequestId);
            signParamsMap.Add("sub_wallet_id", createWeb3Args.SubWalletId?.ToString() ?? "");
            signParamsMap.Add("main_chain_symbol", createWeb3Args.MainChainSymbol);
            signParamsMap.Add("interactive_contract", createWeb3Args.InteractiveContract);
            signParamsMap.Add("amount", createWeb3Args.Amount);
            signParamsMap.Add("input_data", createWeb3Args.InputData);
            return signParamsMap;
        }

        public static string paramsSort(Dictionary<string, string> paramsDict)
        {
            var sortedParams = new SortedDictionary<string, string>();
            foreach (var kvp in paramsDict)
            {
                string key = kvp.Key;
                string value = kvp.Value;
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(value) && key.Equals("amount", StringComparison.OrdinalIgnoreCase))
                {
                    value = Decimal.Parse(value).ToString("0.##########");
                }
                sortedParams[key] = value;
            }
            var sb = new StringBuilder();
            foreach (var param in sortedParams)
            {
                if (!string.IsNullOrWhiteSpace(param.Value))
                {
                    sb.Append("&").Append(param.Key).Append("=").Append(param.Value);
                }
            }
            return sb.ToString().TrimStart('&').ToLower();
        }


        public static string sign(string signData, string signPrivateKey)
        {
            if (string.IsNullOrEmpty(signData)) {
                return "";
            }
            return RsaUtil.Sign(signData, signPrivateKey);
        }
    }
}