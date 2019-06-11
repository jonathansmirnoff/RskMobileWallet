using System;

namespace RSKMobileWallet.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string AddressDisplayed 
        {
            get
            {
                return (string.IsNullOrWhiteSpace(Address) ? "" : string.Format("{0} ... {1}", Address.Substring(0, 6), Address.Substring(Address.Length - 6)));
            } 
        }
        public string Description { get; set; }
    }
}