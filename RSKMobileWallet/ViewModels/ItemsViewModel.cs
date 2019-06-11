using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using RSKMobileWallet.Models;
using RSKMobileWallet.Views;
using RSKMobileWallet.Nethereum.Services;

namespace RSKMobileWallet.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private RskWalletService WalletService { get; set; }

        public ItemsViewModel()
        {
            try
            {

                Title = "Browse";
                Items = new ObservableCollection<Item>();
                LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

                MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
                {
                    var newItem = item as Item;

                    var balance = await WalletService.GetRskBalance(item.Address).ConfigureAwait(false);
                    var balanceRif = await WalletService.GetTokenBalance(item.Address).ConfigureAwait(false);

                    RskRnsResolverService rnsResolverService = new RskRnsResolverService();
                    var address = await rnsResolverService.GetAddress("alecavallero.rsk");

                    newItem.Description = string.Format("RBTC: {0} | RIF: {1}", 
                        string.Format("{0:0.0000}", balance), 
                        string.Format("{0:0.0000}", balanceRif));

                    Items.Add(newItem);
                    await DataStore.AddItemAsync(newItem);
                });

                WalletService = new RskWalletService();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}