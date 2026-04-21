using Core;
using SimpleOrders.Pages.Base;

namespace SimpleOrders.Pages.Parts
{
    public class IndexModel : BasePageModelModel
    {

        public override void  OnGet()
        {
            base.OnGet();
            LoadProducts();
            GetProductsInCart(-1, -1);
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

        void LoadProducts()
        {
            //if (HttpContext.Session.GetInt32("AccountID") != null)
            //{
                //System.Diagnostics.Debug.WriteLine("Loading Products");
                //Get account id and pass that into the product select stored proc

                this.Items = Item.GetItems(Int32.Parse("1"));

                //if (_items != null)
                //    Items = _items;
                //else
                //    Items = new List<Item>();
            //}
        }


        public void OnPostRemoveFromCart(int ItemID, int AccountID, int UserID)
        {
            //int _cartID = 0;
            ////System.Diagnostics.Debug.WriteLine($"Item ID{intItemID} AccountID:{intAccountID}");
            ////Check to see if we have a cart
            //if (HttpContext.Session.GetInt32("CartID") != null)
            //{
            //    _cartID = Int32.Parse(HttpContext.Session.GetInt32("CartID").ToString());
            //    //We don't need to load up the cart, just need to keep track of the ID so we know what
            //    //data we need to pass to the add cart method
            //    Cart userCart = new Cart(_cartID);

            //    if (!userCart.AddToCart( ItemID, 0))
            //        HttpContext.Abort();


            //    OnGet();

            //}
        }

        public void OnGetGetCartContents()
        {
            //System.Diagnostics.Debug.WriteLine("getting cart contents");
            //if (HttpContext.Session.GetInt32("CartID") != null)
            //{
            //    this.CartedItems =  Cart.GetCartedItems((int)HttpContext.Session.GetInt32("UserID"),(int)HttpContext.Session.GetInt32("CartID"));
            //    //We don't need to load up the cart, just need to keep track of the ID so we know what
            //    //data we need to pass to the add cart method

            //    OnGet();

            //}
        }
        public void OnPostAddToCart(int ItemID, int AccountID, int UserID)
        {
            //int _cartID = 0;
            ////System.Diagnostics.Debug.WriteLine($"Item ID{intItemID} AccountID:{intAccountID}");
            ////Check to see if we have a cart
            //if (HttpContext.Session.GetInt32("CartID") != null)
            //{
            //    _cartID = Int32.Parse(HttpContext.Session.GetInt32("CartID").ToString());
            //    //We don't need to load up the cart, just need to keep track of the ID so we know what
            //    //data we need to pass to the add cart method
            //    Cart userCart = new Cart(_cartID);
            //    if (!userCart.AddToCart(ItemID, 1))
            //        HttpContext.Abort();
            //}
            //else
            //{
            //    //If we don't have a cart yet, we just need to pass in the account ID and the item
            //    //And let our back end create the cart iD from us, then we can just shove that into session on the way back down
            //    _cartID = Cart.Create(ItemID, AccountID, UserID); //This will return the cart id.
            //    if (_cartID != 0)
            //        HttpContext.Session.SetInt32("CartID", _cartID);
            //}

            ////LoadProducts();
            ////if(_cartID > 0) //Just make sure we have a valid cart ID
            ////  GetProductsInCart(UserID, _cartID);
            //OnGet();
        }


       // public string? UserName { get; protected set; }
        public List<Item>? Items { get; protected set; }
        //public int UserID { get; protected set; }

        public List<Item>? CartedItems { get; protected set; }
    }
}
