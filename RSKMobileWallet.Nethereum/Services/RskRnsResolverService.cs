﻿using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.ENS;
using Nethereum.ENS.ENSRegistry.ContractDefinition;
using Nethereum.Hex.HexConvertors.Extensions;

namespace RSKMobileWallet.Nethereum.Services
{
    public class RskRnsResolverService : IRskRnsResolverService
    {
        private string RskMainnetNodeUrl { get; set; } = "https://public-node.rsk.co";
        private string RnsRegistry { get; set; } = "0xcb868aeabd31e2b66f74e9a55cf064abb31a4ad5";
        private Web3 Web3Client { get; set; }

        public RskRnsResolverService()
        {
            Web3Client = new Web3(RskMainnetNodeUrl);
        }

        public async Task<string> GetAddress(string accountDomain)
        {
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
    }
}