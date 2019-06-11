using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.StandardTokenEIP20;

namespace RSKMobileWallet.Nethereum.Services
{
    public class RskWalletService : IRskWalletService
    {
        private string RskTestnetNodeUrl { get; set; } = "https://public-node.testnet.rsk.co";
        private string RskTestnetRifAddress { get; set; } = "0xd8c5adcac8d465c5a2d0772b86788e014ddec516";
        private Web3 Web3Client { get; set; }

        public RskWalletService()
        {
            Web3Client = new Web3(RskTestnetNodeUrl);
        }

        public async Task<decimal> GetRskBalance(string accountAddress)
        {
            var weiBalance = await Web3Client.Eth.GetBalance.SendRequestAsync(accountAddress).ConfigureAwait(false);
            return Web3.Convert.FromWei(weiBalance);
        }

        public async Task<decimal> GetTokenBalance(string accountAddress)
        {
            var service = new StandardTokenService(Web3Client, RskTestnetRifAddress);
            var wei = await service.BalanceOfQueryAsync(accountAddress).ConfigureAwait(false);
            return Web3.Convert.FromWei(wei, 18);
        }

    }
}