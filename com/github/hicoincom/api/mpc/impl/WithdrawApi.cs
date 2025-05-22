using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;
using com.github.hicoincom.exception;
using com.github.hicoincom.util;

namespace com.github.hicoincom.api.mpc.impl
{
    public class WithdrawApi : WaasApi, IWithdrawApi
    {

        public WithdrawApi(WaasConfig config, IDataCrypto dataCrypto) : base(config, dataCrypto)
        {
        }

        public WithdrawRecordResult getWithdrawRecords(List<string> requestIds)
        {
            if (requestIds == null || !requestIds.Any())
            {
                throw new ArgsNullException("get withdraw records, the request parameter 'ids' empty");
            }
            var args = new GetTransactionRecordArgs
            {
                Ids = string.Join(",", requestIds)
            };
            return Invoke<WithdrawRecordResult>(MpcApiUri.WITHDRAW_RECORDS, args, typeof(WithdrawRecordResult));
        }

        public WithdrawRecordResult syncWithdrawRecords(int? maxId)
        {
            var args = new SyncTransactionRecordArgs
            {
                MaxId = maxId ?? 0
            };
            return Invoke<WithdrawRecordResult>(MpcApiUri.SYNC_WITHDRAW_RECORDS, args, typeof(WithdrawRecordResult));
        }

        public WithdrawResult withdraw(WithdrawArgs? withdrawArgs, bool needTransactionSign)
        {
            if (withdrawArgs == null)
            {
                throw new ArgsNullException("mpc withdraw args empty");
            }
            string priKey = ((MpcConfig) Config).SignPrivateKey;
            if (needTransactionSign && string.IsNullOrEmpty(priKey))
            {
                throw new ConfigException("configure 'signPrivateKey' as empty");
            }
            if (needTransactionSign)
            {
                Dictionary<string, string> signParamsMap = MpcSignUtil.getWithdrawSignParams(withdrawArgs);
                string signData = MpcSignUtil.paramsSort(signParamsMap);
                if (string.IsNullOrEmpty(signData))
                {
                    throw new CryptoException("mpc create web3 transaction, parameter signing failed");
                }
                string sign = MpcSignUtil.sign(signData, priKey);
                if (string.IsNullOrEmpty(sign))
                {
                    throw new CryptoException("mpc create web3 transaction, sign parameter error");
                }
                withdrawArgs.Sign = sign;
            }
            return Invoke<WithdrawResult>(MpcApiUri.BILLING_WITHDRAW, withdrawArgs, typeof(WithdrawResult));
        }

        public WithdrawResult withdraw(WithdrawArgs? withdrawArgs)
        {
            if (withdrawArgs == null)
            {
                throw new ArgsNullException("mpc withdraw args empty");
            }
            return Invoke<WithdrawResult>(MpcApiUri.BILLING_WITHDRAW, withdrawArgs, typeof(WithdrawResult));
        }
    }
}