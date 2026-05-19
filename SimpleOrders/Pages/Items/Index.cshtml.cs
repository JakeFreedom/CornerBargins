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
            //GetProductsInCart(-1, -1);
        }

         void GetProductsInCart(int UserID, int cartID)
        {
            //if (UserID > 0 && cartID > 0)
            //{
            //    this.CartedItems = Cart.GetCartedItems(UserID, cartID);
            //    return;
            //}

            //if (HttpContext.Session.GetInt32("CartID") != null)
            //{
            //    this.CartedItems = Cart.GetCartedItems((int)HttpContext.Session.GetInt32("UserID"), (int)HttpContext.Session.GetInt32("CartID"));
            //    return;
            //}

           // this.CartedItems = new List<Item>();

        }

        void LoadProducts(bool fullLoad=true)
        {

            if (fullLoad)
                this.Items = Item.GetItems();
            else
                this.Items = new List<Item>();

        }

        void LoadMedia(){ }

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

        public List<Item>? CartedItems { get; protected set; }

        public string Image { get; protected set; }
        public string Name { get; protected set; }
    }
}
