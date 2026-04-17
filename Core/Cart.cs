using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Core
{
    public class Cart
    {
        //What will a cart have
        //Item Collection
        //ID
        //Owner -- Account ID

        public Cart(int cartID)
        {
            //Got get everythign for this cart. Which is basically just the items
            this.CartID = cartID;
        }
 
        public static List<Item> GetCartedItems(int userID, int cartID)
        {
            //Create our return list
            List<Item> cartedItems = new List<Item>();
            //Setup DB call
            DataManager dm = new DataManager();
            Dictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("@intCartID", cartID.ToString());
            dm.GetDataProc("CartedItem_S", parameters);
            if(dm.RowsAffected>0)
            {
                DataTable dataTable = new DataTable();
                dataTable = dm.DBData.Tables[0];
                DataView dv = new DataView(dataTable);
                foreach(DataRowView drv in dv)
                {
                    //Iterate through the returned items
                    Item i = new Item(Int32.Parse(drv["intItemID"].ToString()), drv["vcItemNumber"].ToString(), drv["vcItemDescription"].ToString(), Int32.Parse(drv["intQuantity"].ToString()));
                    cartedItems.Add(i);
                }

                return cartedItems;
            }
            //Return a blank list if there was nothing found
            return new List<Item>();  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AccountID">ComEdge Account ID -- Not Index ID of the account table</param>
        /// <returns></returns>
        public static int GetUserCart(int UserID, int AccountID)
        {
            DataManager dm = new DataManager();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@intUserID", UserID.ToString());
            param.Add("@intAccountID", AccountID.ToString());
            if(dm.GetDataProc("Cart_GetUserCartID", param))
            {
                DataTable dt = new DataTable("CartID");
                dt = dm.DBData.Tables[0];
                DataView dv = new DataView(dt);

                if (dv.Count <= 0)
                    return -1;

                DataRowView cartRow = dv[0];
                int CartID = Int32.Parse(cartRow["intID"].ToString());
                if (CartID > 0)
                    return CartID;
            }

            return -1;
        }

        public bool AddToCart(int ItemID, int Quantity)
        {
            DataManager dm = new DataManager();
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("@intCartID", this.CartID.ToString());
            parms.Add("@intItemID", ItemID.ToString());
            parms.Add("@intQuantity", Quantity.ToString());
            int reutrnCartID = dm.InsertDataProc<int>("CartedItem_IU", parms);
            if (reutrnCartID == this.CartID)//Make sure the proc ran correctly by returning the cartID that was sent in. Not sure that is the best way, but it works on the surface.
                return true;

            return false;
        }

        public static int Create(int ItemID, int AccountID, int UserID)
        {
            DataManager dm = new DataManager();
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("@intAccountID", AccountID.ToString());
            parms.Add("@intUserID", UserID.ToString());
            parms.Add("@intItemID", ItemID.ToString());
            return  dm.InsertDataProc<int>("Cart_I", parms);

        }

        public static int Create(int AccountID, int UserID)
        {
            DataManager dm = new DataManager();
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("@intAccountID", AccountID.ToString());
            parms.Add("@intUserID", UserID.ToString());
            return dm.InsertDataProc<int>("Cart_I", parms);
        }

        public static int PlaceOrder(int? AccountID, int? CartID)
        {
            DataManager dm = new DataManager();
            Dictionary<string,string> parameters = new Dictionary<string, string>();
            parameters.Add("@intAccountID", AccountID.ToString());
            parameters.Add("@intCartID", CartID.ToString());

            return dm.InsertDataProc<int>("Cart_PlaceOrder", parameters);
        }

        public static int PlaceOrder(int? AccountID, int? CartID, string PONumber, string Comments, string Email, string ShipToAddress)
        {
            DataManager dm = new DataManager();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@intAccountID", AccountID.ToString());
            parameters.Add("@intCartID", CartID.ToString());
            parameters.Add("@vcPONumber", PONumber);
            parameters.Add("@vcComments", Comments);
            parameters.Add("@vcEmailAddress", Email);
            parameters.Add("@vcShipToAddress", ShipToAddress);

            return dm.InsertDataProc<int>("Cart_PlaceOrder", parameters);
        }

        public List<Item> CartedItems { get; protected set; }
        public int AccountID { get; protected set; }
        public int CartID { get; protected set; }  
    }
}
