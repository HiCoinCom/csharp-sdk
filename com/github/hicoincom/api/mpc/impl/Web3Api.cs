using com.github.hicoincom.api.bean;
using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;
using com.github.hicoincom.exception;
using com.github.hicoincom.util;

namespace com.github.hicoincom.api.mpc.impl
{
    public class Web3Api : WaasApi, IWeb3Api
    {
        public Web3Api(WaasConfig config, IDataCrypto dataCrypto) : base(config, dataCrypto) { }

        public bool accelerationWeb3Trans(Web3AccelerationArgs? accelerationArgs)
        {
            if (accelerationArgs == null)
            {
                throw new ArgsNullException("acceleration web3 transaction args empty");
            }
            var result = Invoke<Result<object>>(MpcApiUri.WEB3_TRANS_ACCELERATION, accelerationArgs, typeof(Result<object>));
            return result != null && result.GetCode().Equals("0", StringComparison.OrdinalIgnoreCase);
        }

        public CreateWeb3Result createWeb3Trans(CreateWeb3Args? web3TransArgs, bool needTransactionSign)
        {
            if (web3TransArgs == null)
            {
                throw new ArgsNullException("mpc create web3 transaction args empty");
            }
            string priKey = ((MpcConfig)Config).SignPrivateKey;
            if (needTransactionSign && string.IsNullOrEmpty(priKey))
            {
                throw new ConfigException("configure 'signPrivateKey' as empty");
            }
            if (needTransactionSign)
            {
                Dictionary<string, string> signParamsMap = MpcSignUtil.getWeb3SignParams(web3TransArgs);
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
                web3TransArgs.Sign = sign;
            }
            return Invoke<CreateWeb3Result>(MpcApiUri.CREATE_WEB3_TRANSACTION, web3TransArgs, typeof(CreateWeb3Result));
        }

        public CreateWeb3Result createWeb3Trans(CreateWeb3Args? web3TransArgs)
        {
            if (web3TransArgs == null)
            {
                throw new ArgsNullException("mpc create web3 transaction args empty");
            }
            return Invoke<CreateWeb3Result>(MpcApiUri.CREATE_WEB3_TRANSACTION, web3TransArgs, typeof(CreateWeb3Result));
        }

        public Web3RecordResult getWeb3Records(List<string> requestIds)
        {
            if (requestIds == null || !requestIds.Any())
            {
                throw new ArgsNullException("get web3 records, the request parameter 'ids' empty");
            }
            var args = new GetTransactionRecordArgs
            {
                Ids = string.Join(",", requestIds)
            };
            return Invoke<Web3RecordResult>(MpcApiUri.WEB3_TRANS_RECORDS, args, typeof(Web3RecordResult));
        }

        public Web3RecordResult syncWeb3Records(int? maxId)
        {
            var args = new SyncTransactionRecordArgs
            {
                MaxId = maxId ?? 0
            };
            return Invoke<Web3RecordResult>(MpcApiUri.SYNC_WEB3_RECORDS, args, typeof(Web3RecordResult));
        }
    }
}