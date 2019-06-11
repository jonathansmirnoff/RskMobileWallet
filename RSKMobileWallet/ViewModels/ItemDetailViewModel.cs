using System;

using RSKMobileWallet.Models;

namespace RSKMobileWallet.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.AddressDisplayed;
            Item = item;
        }
    }
}
