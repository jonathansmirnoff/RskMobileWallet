using System;
using System.Threading.Tasks;

namespace RSKMobileWallet.Nethereum.Services
{
    public interface IRskWalletService
    {
        Task<decimal> GetRskBalance(string accountAddress);
        Task<decimal> GetTokenBalance(string accountAddress);
    }
}