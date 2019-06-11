using System;
using System.Threading.Tasks;

namespace RSKMobileWallet.Nethereum.Services
{
    public interface IRskRnsResolverService
    {
        Task<string> GetAddress(string accountDomain);
    }
}
