using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;

namespace com.github.hicoincom.api.mpc.impl
{
    public class DepositApi : WaasApi, IDepositApi
    {
        public DepositApi(WaasConfig config, IDataCrypto dataCrypto) : base(config, dataCrypto)
        {
        }


        public DepositRecordResult syncDepositRecords(int? maxId)
        {
            var args = new SyncTransactionRecordArgs
            {
                MaxId = maxId ?? 0
            };
            return Invoke<DepositRecordResult>(MpcApiUri.SYNC_DEPOSIT_RECORDS, args, typeof(DepositRecordResult));
        }

        public DepositRecordResult getDepositRecords(List<int> ids)
        {
            if (ids == null || !ids.Any()) {
                throw new ArgumentException("the request parameter 'ids' empty");
            }
            var args = new GetTransactionRecordArgs
            {
                Ids = string.Join(",", ids)
            };
            return Invoke<DepositRecordResult>(MpcApiUri.DEPOSIT_RECORDS, args, typeof(DepositRecordResult));
        }
    }
}