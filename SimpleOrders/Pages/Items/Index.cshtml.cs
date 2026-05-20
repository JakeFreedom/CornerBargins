using Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleOrders.Pages.Base;

namespace CB.Pages.Items
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
            LoadProducts();
            LoadMedia();
        }


        void LoadProducts(bool fullLoad=true)
        {

            if (fullLoad)
                this.Items = Item.GetItems();
            else
                this.Items = new List<Item>();

        }

        void LoadMedia()
        {
            this.ItemIcons = new MediaCollection(Core.ENUMS.ImageType.ICON);
        }

        public void OnPostShowFullImage(int itemID, string itemName)
        {
            LoadProducts(false);

            byte[] itemImage = Item.GetItemImage(itemID);
            this.Image = string.Format("data:image/" + "png" + ";base64,{0}", Convert.ToBase64String(new Item().GetFullImage(itemImage, 1200,900)));
            this.Name = itemName.Replace('-', ' ');
        }

       // public string? UserName { get; protected set; }
        public List<Item>? Items { get; protected set; }
        //public int UserID { get; protected set; }

        public MediaCollection ItemIcons { get; protected set; }

        public string Image { get; protected set; }
        public string Name { get; protected set; }
    }
}
