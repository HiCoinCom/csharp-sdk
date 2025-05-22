using com.github.hicoincom.api.mpc;

namespace com.github.hicoincom
{
    public class MpcClient {
        public IWorkSpaceApi WorkSpaceApi { get; set; }

        public IWalletApi WalletApi { get; set; }

        public IWithdrawApi WithdrawApi { get; set; }

        public IAutoSweepApi AutoSweepApi { get; set; }

        public IWeb3Api Web3Api { get; set; }

        public IDepositApi DepositApi { get; set; }
    }
}