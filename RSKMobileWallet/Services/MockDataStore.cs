using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSKMobileWallet.Models;
using RSKMobileWallet.Nethereum.Services;

namespace RSKMobileWallet.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;
        private RskWalletService WalletService { get; set; }

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Address = "0x179B7bc37b155f9D9A0Bd96bB5356CeE45f998eD", Description="This is an item description." },
                /*
                new Item { Id = Guid.NewGuid().ToString(), Address = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Address = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Address = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Address = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Address = "Sixth item", Description="This is an item description." }*/
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }

            WalletService = new RskWalletService();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {        
            foreach (var item in items)
            {
                var balance = await WalletService.GetRskBalance(item.Address).ConfigureAwait(false);
                var balanceRif = await WalletService.GetTokenBalance(item.Address).ConfigureAwait(false);

                item.Description = string.Format("RBTC: {0} | RIF: {1}",
                    string.Format("{0:0.0000}", balance),
                    string.Format("{0:0.0000}", balanceRif));
            }

            return await Task.FromResult(items);
        }
    }
}