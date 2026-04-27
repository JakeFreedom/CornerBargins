using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core;

namespace SimpleOrders.Pages
{
    public class IndexModel : CB.Pages.Base.BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public override void OnGet()
        {
            //if(HttpContext.Session.GetString("isLoggedIn") == "true")
            Response.Redirect("/Items");   

        }

        //public void OnGetCheckLogin(string strUserName, string strAccountID)
        //{
        //    System.Diagnostics.Debug.WriteLine(strUserName);
        //    int userID = 1;// Core.User.CheckLogin(strUserName, strAccountID);
        //    if(userID != 0)
        //    {
        //        HttpContext.Session.SetString("isLoggedIn", "true");
        //        HttpContext.Session.SetString("UserName", "brad");
        //        HttpContext.Session.SetInt32("AccountID",1);//This isn't the index of the Account table, this is the users ComEdge Account ID
        //        HttpContext.Session.SetInt32("UserID", 1);

        //        //Get get cart if the user has one. -- If we do this, then we will need to have a system in place that when the order is submitted, that we
        //        //Clear out the cart and the cartedItems, or in some fashion mark the cartedItems as completed.
        //        int CartID = Cart.GetUserCart(userID, Int32.Parse("1"));
        //        if (CartID != -1)
        //        {
        //            HttpContext.Session.SetInt32("CartID", CartID);
        //        }
        //        else
        //        {
        //            CartID = Cart.Create(userID, Int32.Parse(strAccountID));
        //            HttpContext.Session.SetInt32("CartID", CartID);
        //        }
        //    }
        //}
    }
}
