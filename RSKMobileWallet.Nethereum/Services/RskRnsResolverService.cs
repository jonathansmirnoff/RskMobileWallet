using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.ENS;
using Nethereum.ENS.ENSRegistry.ContractDefinition;
using Nethereum.Hex.HexConvertors.Extensions;

namespace RSKMobileWallet.Nethereum.Services
{
    public class RskRnsResolverService : IRskRnsResolverService
    {
        private static string RskMainnetNodeUrl { get; } = "https://public-node.rsk.co";
        private string RnsRegistry { get; } = "0xcb868aeabd31e2b66f74e9a55cf064abb31a4ad5";
        private Web3 Web3Client { get; }

        public RskRnsResolverService() : this(new Web3(RskMainnetNodeUrl)) { }

        public RskRnsResolverService(Web3 web3Client)
        {
            Web3Client = web3Client;
        }

        public async Task<string> GetAddress(string accountDomain)
        {
            if (!IsValidDomain(accountDomain)) throw new ArgumentException("Invalid name.", nameof(accountDomain));
            try
            {           
                var ensRegistryService = new ENSRegistryService(Web3Client, RnsRegistry);
                var fullNameNode = new EnsUtil().GetNameHash(accountDomain);

                var resolverAddress = await ensRegistryService.ResolverQueryAsync(
                    new ResolverFunction() { Node = fullNameNode.HexToByteArray() }).ConfigureAwait(false);

                var resolverService = new PublicResolverService(Web3Client, resolverAddress);
                var theAddress =
                    await resolverService.AddrQueryAsync(fullNameNode.HexToByteArray()).ConfigureAwait(false);

                return theAddress;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private static bool IsValidDomain(string accountDomain)
        {
            //TODO: implement
            return true;
        }
    }
}