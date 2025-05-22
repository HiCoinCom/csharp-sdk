using com.github.hicoincom.api.bean.mpc;

namespace com.github.hicoincom.api.mpc
{
    public interface IWeb3Api
    {
        /**
         * Create Web3 Transaction
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/web3/web3-create
         *
         * @param web3TransArgs       required true
         *                            create web3 transaction args
         * @param needTransactionSign transactions require signature fields
         */
        CreateWeb3Result createWeb3Trans(CreateWeb3Args? web3TransArgs, bool needTransactionSign);

        /**
         * Create Web3 Transaction
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/web3/web3-create
         *
         * @param web3TransArgs required true
         *                      create web3 transaction args
         */
        CreateWeb3Result createWeb3Trans(CreateWeb3Args? web3TransArgs);

        /**
         * Web3 Transaction Acceleration
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/web3/web3-pending
         *
         * @param accelerationArgs required true
         *                         acceleration web3 transaction args
         */
        bool accelerationWeb3Trans(Web3AccelerationArgs? accelerationArgs);


        /**
         * Get Web3 Transaction Records
         * Get all Web3 transaction records under a wallet, maximum of 100 records.
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/web3/web3-record-list
         *
         * @param requestIds required: true
         *                   many request_id string
         */
        Web3RecordResult getWeb3Records(List<string> requestIds);

        /**
         * Sync Web3 Transaction Records
         * Get all Web3 transaction records under a wallet, maximum of 100 records.
         * https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/web3/web3-record-sync-list
         *
         * @param maxId required: true
         *              Starting ID of Web3 transactions, default:0
         */
        Web3RecordResult syncWeb3Records(int? maxId);
    }
}