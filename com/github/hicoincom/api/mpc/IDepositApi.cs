using com.github.hicoincom.api.bean.mpc;

namespace com.github.hicoincom.api.mpc
{
    public interface IDepositApi
    {
        /**
         * Get Receiving records
         * Get all wallet receiving records under the workspace, and return up to 100 records
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/deposit/deposit-list
         *
         * @param ids required: true
         *            Receiving id
         */
        DepositRecordResult getDepositRecords(List<int> ids);

        /**
         * Synchronize Transfer(deposit) Records
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/deposit/deposit-sync-list
         *
         * @param maxId required: true
         *              receiving record initial id, e.g.：100
         */
        DepositRecordResult syncDepositRecords(int? maxId);
    }
}