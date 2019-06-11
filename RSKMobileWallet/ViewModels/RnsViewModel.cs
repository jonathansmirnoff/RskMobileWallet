using System;
using System.Threading.Tasks;
using RSKMobileWallet.Nethereum.Services;

namespace RSKMobileWallet.ViewModels
{
    public class RnsViewModel
    {
        private RskRnsResolverService RnsService { get; set; }

        public RnsViewModel()
        {
            RnsService = new RskRnsResolverService();
        }

        public async Task<string> ResolveDomain(string domain)
        {
            return await RnsService.GetAddress(domain).ConfigureAwait(false);
        }
    }
}