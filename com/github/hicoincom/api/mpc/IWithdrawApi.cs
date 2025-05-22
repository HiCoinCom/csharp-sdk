using com.github.hicoincom.api.bean.mpc;

namespace com.github.hicoincom.api.mpc
{
    public interface IWithdrawApi
    {
        /**
         * Transfer (Withdrawal)
         * Initiate a Transfer
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/withdraw/withdraw
         *
         * @param withdrawArgs        required true
         *                            withdraw info args
         * @param needTransactionSign transactions require signature fields
         */
        WithdrawResult withdraw(WithdrawArgs? withdrawArgs, bool needTransactionSign);

        /**
         * Transfer (Withdrawal)
         * Initiate a Transfer
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/withdraw/withdraw
         *
         * @param withdrawArgs required true
         *                     withdraw info args
         */
        WithdrawResult withdraw(WithdrawArgs? withdrawArgs);

        /**
         * Get Transfer Records
         * Get all wallet transfer records under the workspace, and return up to 100 records
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/withdraw/withdraw-list
         *
         * @param requestIds required: true
         *                   many request_id string
         */
        WithdrawRecordResult getWithdrawRecords(List<string> requestIds);

        /**
         * Synchronize Transfer(withdraw) Records
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/withdraw/withdraw-sync-list
         *
         * @param maxId required: true
         *              transfer record initial id, default: 0
         */
        WithdrawRecordResult syncWithdrawRecords(int? maxId);
    }
}