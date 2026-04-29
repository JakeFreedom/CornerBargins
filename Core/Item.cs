using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
    public class Item
    {
        public Item() { }

        /// <summary>
        /// Go get an item by ID
        /// </summary>
        /// <param name="ID"></param>
        public Item(int ID) {

            

        }

        public Item(int ID, string Name, string Description, string Cost, string Link, int Status, byte[] Image, DateTime LastUpdated)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Cost = Cost;
            this.Link = Link;
            this.Image = Image;
            this.Status = Status;
            this.Updated = LastUpdated;
        }

        //Get all items a user has access to.
        public static List<Item> GetItems()
        {
            List<Item> returnList = new List<Item>();
            DataManager dm = new DataManager();

            dm.GetDataProc("Item_S");
            if (dm.RowsAffected > 0)
            {
                DataTable dt = dm.DBData.Tables[0];
                DataView dv = new DataView(dt);
                foreach(DataRowView drv in dv)
                {
                    Item i = new Item(Int32.Parse(drv["intID"].ToString()), drv["vcName"].ToString(), drv["vcDescription"].ToString(),
                        decimal.Parse(drv["decCost"].ToString()).ToString(), drv["vcExternalLink"].ToString(), Int32.Parse(drv["intStatus"].ToString()), (byte[])drv["binImage"],
                        DateTime.Parse(drv["dtUpdated"].ToString()));

                    returnList.Add(i);
                }
                
            }

            if (returnList.Count > 0)
                return returnList;
            else
                return new List<Item>();

        }

        public static byte[] GetItemImage(int ID)
        {
            byte[] itemImage = null;
            DataManager dm = new DataManager();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("intItemID", ID.ToString());
            dm.GetDataProc("Item_SFullImage", param);

            if(dm.RowsAffected > 0)
            {
                DataTable dt = dm.DBData.Tables[0];
                DataView dv = new DataView(dt);

                itemImage = (byte[])dv[0][0];

            }

            return itemImage;
        }


        public static int AddItem(byte[] image, string name, string description, decimal cost, string link, string labelColor)
        {

            DataManager dm = new DataManager();
            List<SqlParameter> sp = new List<SqlParameter>();

            sp.Add(new SqlParameter("binImage", image));
            sp.Add(new SqlParameter("vcName", name));
            sp.Add(new SqlParameter("vcDescription", description));
            sp.Add(new SqlParameter("intStatus", 1));
            sp.Add(new SqlParameter("decCost", cost));
            sp.Add(new SqlParameter("vcExternalLink", link));

            return dm.InsertDataProc<int>("Item_I", sp);
        }

        public static int UpdateItemImage(byte[] newImage, int itemID) 
        {
            DataManager dm = new DataManager();
            List<SqlParameter> sp = new List<SqlParameter>();

            sp.Add(new SqlParameter("binImage", newImage));
            sp.Add(new SqlParameter("intID", itemID));

            return dm.InsertDataProc<int>("Item_U", sp);
        }


        private bool ThumbnailCallback() { return false; }
        public byte[] GetResizedImage(int maxHeight, int maxWidth)
        {
            //Image thumb = i.GetThumbnailImage(128, 128, null, IntPtr.Zero);
            //thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            MemoryStream ms = new MemoryStream(this.Image);
            Image i = System.Drawing.Image.FromStream(ms);

            double aspectRatio = (double)i.Width / i.Height;
            int _height = maxHeight;
            int _width = maxWidth;
            Bitmap b = null;
            if (aspectRatio > 1)
                _height = (int)(_width / aspectRatio);
            else
                _width = (int)(_height * aspectRatio);

            b = new Bitmap(i, _width, _height);

            
            Image myThumbnail = b.GetThumbnailImage(_width,_height, myCallback, IntPtr.Zero);
            //myThumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);


            myThumbnail.Save($"C:\\pwp\\{this.Name}.png");

            //return ms.ToArray();
            return File.ReadAllBytes($"C:\\pwp\\{this.Name}.png");
        }

        public byte[] GetFullImage(byte[] image, int maxWidth, int maxHeight)
        {
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            MemoryStream ms = new MemoryStream(image);
            Image i = System.Drawing.Image.FromStream(ms);

            double aspectRatio = (double)i.Width / i.Height;
            int _height = maxHeight;
            int _width = maxWidth;
            Bitmap b = null;
            if (aspectRatio > 1)
                _height = (int)(_width / aspectRatio);
            else
                _width = (int)(_height * aspectRatio);

            b = new Bitmap(i, _width, _height);


            Image myThumbnail = b.GetThumbnailImage(_width, _height, myCallback, IntPtr.Zero);


            myThumbnail.Save($"C:\\pwp\\temp.png");

            //return ms.ToArray();
            return File.ReadAllBytes($"C:\\pwp\\temp.png");
        }


        public int ID { get; protected set; }
        public string? Name { get; protected set; }    
        public string? Description { get; protected set; }
       
       public byte[] Image { get; protected set; }

        public string Cost { get; protected set; }
        public string Link { get; protected set; }
        public int Status { get; protected set; } //Double as to what the label color will be
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }
    }
}
