using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.ComponentModel;


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

        public Item(int ID, string Name, string Description, string Cost, string Link, string LabelColor, string Image)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Cost = Cost;
            this.Link = Link;
            this.Image = Image;
        }

        //Get all items a user has access to.
        public static List<Item> GetItems(int accountID)
        {
            List<Item> returnList = new List<Item>();
            DataManager dm = new DataManager();
            Dictionary<string, string> parameters = new Dictionary<string, string>();


            dm.GetDataProc("Item_S");
            if (dm.RowsAffected > 0)
            {
                DataTable dt = dm.DBData.Tables[0];
                DataView dv = new DataView(dt);
                foreach(DataRowView drv in dv)
                {
                    Item i = new Item(Int32.Parse(drv["intID"].ToString()), drv["vcName"].ToString(), drv["vcDescription"].ToString(), 
                        decimal.Parse(drv["decCost"].ToString()).ToString(), drv["vcExternalLink"].ToString(), "green", drv["binImage"].ToString());

                    returnList.Add(i);
                }
                
            }

            if (returnList.Count > 0)
                return returnList;
            else
                return new List<Item>();

        }


        public static int AddItem(string image, string name, string description, decimal cost, string link, string labelColor)
        {
            DataManager dm = new DataManager();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("binImage", image.ToString());
            parameters.Add("vcName", name);
            parameters.Add("vcDescription", description);
            parameters.Add("intStatus", "1");
            parameters.Add("decCost", cost.ToString());
            parameters.Add("vcExternalLink", link);

            return dm.InsertDataProc<int>("Item_I", parameters);
        }

        private bool ThumbnailCallback() { return false; }
        public byte[] GetResizedImage()
        {
    
            //Image thumb = i.GetThumbnailImage(128, 128, null, IntPtr.Zero);
            //thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(this.Image));
            Image i = System.Drawing.Image.FromStream(ms);

            //Figure out what our heights and widths need to be
            //First lets find out resolution
            decimal res = (i.Height > i.Width ? ((decimal)i.Width / (decimal)i.Height) :((decimal)i.Height/(decimal)i.Width));
            int _height = i.Height;
            int _width = i.Width;

            if (_height > _width)
            {
                while (_height * res > 300)
                {
                    decimal value = (decimal)_height * res;
                    _height = (int)value;
                    value = (decimal)_width * res;
                    _width = (int)value;

                }
            }
            else
            {
                while (_width * res > 300)
                {
                    decimal value = (decimal)_width * res -1;
                    _width = (int)value;
                    value = (decimal)_height * res -1;
                    _height = (int)value;
                    //if (res == 1)
                        //break;
                }
            }
            
            Bitmap b = new Bitmap(i);
            
            Image myThumbnail = b.GetThumbnailImage(_width,_height, myCallback, IntPtr.Zero);
            //myThumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);


            myThumbnail.Save($"C:\\pwp\\{this.Name}.png");

            //return ms.ToArray();
            return File.ReadAllBytes($"C:\\pwp\\{this.Name}.png");
        }

        public int ID { get; protected set; }
        public string? Name { get; protected set; }    
        public string? Description { get; protected set; }
       //public byte[]? Image { get; protected set; }
       public string Image { get; protected set; }

        public string Cost { get; protected set; }
        public string Link { get; protected set; }
        public int Status { get; protected set; } //Double as to what the label color will be
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }
    }
}
