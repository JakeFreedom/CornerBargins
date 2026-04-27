using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleOrders.Pages.Base;

namespace SimpleOrders.Pages.ShoppingCart
{
    public class IndexModel : CB.Pages.Base.BasePageModel
    {
        public override void OnGet()
        {
            base.OnGet();

            //LoadCartedItems();
        }
        
        private void LoadCartedItems()
        {
            //if (HttpContext.Session.GetInt32("CartID") == null)
            //{
            //    AccountID = (int)HttpContext.Session.GetInt32("AccountID");
            //    UserID = (int)HttpContext.Session.GetInt32("UserID");
            //    CartID = Core.Cart.Create(AccountID, UserID);
            //}
            //else
            //{
            //    UserID = (int)HttpContext.Session.GetInt32("UserID");
            //    CartID = (int)HttpContext.Session.GetInt32("CartID");
            //    AccountID = (int)HttpContext.Session.GetInt32("AccountID");
            //}
            //    this.CartedItems = Core.Cart.GetCartedItems(UserID, CartID);
        }

       public void OnPostUpdateCart(int ItemID, int AccountID, int UserID, int Quantity)
        {
            //int CartID = Int32.Parse(HttpContext.Session.GetInt32("CartID").ToString());
            //Core.Cart c = new Core.Cart(CartID);
            //if (!c.AddToCart(ItemID, Quantity))
            //    HttpContext.Abort();//Fail the repost --Update in site.js

            //OnGet();
        }

        public void OnPostPlaceOrder(string PONumber, string Comments, string Email, string CustomerNumber, string ShipToAddress)
        {
            //if(Comments != null)
            //    Comments = Comments.Replace("\n", "<br />");

            ////Replace line breaks with BR for the HTML email that is sent off
            //ShipToAddress = ShipToAddress.Replace("\n", "<br />");
            
            //int? AccountID = HttpContext.Session.GetInt32("AccountID");
            //int? CartID = HttpContext.Session.GetInt32("CartID");

            //if (Int32.Parse(CustomerNumber) != AccountID)
            //{
            //    HttpContext.Abort();//This will cause a fail in javascript
            //    return;
            //}

            //if(Core.Cart.PlaceOrder(AccountID, CartID, PONumber, Comments, Email, ShipToAddress) == AccountID)
            //    HttpContext.Session.Remove("CartID");
        }

        public List<Core.Item> CartedItems { get; protected set; }

        public int UserID { get; protected set; }
        public int CartID { get; protected set; }
        public int AccountID { get; protected set; }
       
    }

}
