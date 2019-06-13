using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSKMobileWallet.Nethereum.Services;

namespace RSKMobileWallet.Tests
{
    [TestClass]
    public class RskWalletServiceTests
    {
        private IRskRnsResolverService _rns;

        [TestInitialize]
        public void Init()
        {
            _rns = new RskRnsResolverService();
        }

        [TestMethod]
        public void WhenResolvingKnownDomainThenResolves()
        {
            const string addressToMatch = "0x2c4a4a346ddc3122c2628dbe9dbbca1bbd59551c";
            const string domain = "consensus.rsk";
            var address = _rns.GetAddress(domain).Result;

            Console.WriteLine(address);

            Assert.AreEqual(address, addressToMatch);
        }
    }
}
