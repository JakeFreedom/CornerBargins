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

        public Item(int ID, string PartNumber, string Description, byte[] Image)
        {
            this.ID = ID;
            this.Name = PartNumber;
            this.Description = Description;
            this.Image = Image;
        }


        public Item(int ID, string PartNumber, string Description, int Quantity)
        {
            this.ID = ID;
            this.Name = PartNumber;
            this.Description = Description;
            this.Quantity = Quantity;
        }

        //Get all items a user has access to.
        public static List<Item> GetItems(int accountID)
        {
            List<Item> returnList = new List<Item>();
            DataManager dm = new DataManager();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@vcComEdgeAccountNumber", accountID.ToString());
            if(dm.GetDataProc("ItemS_ByAccountID", parameters))
            {
                DataTable dt = dm.DBData.Tables[0];
                DataView dv = new DataView(dt);
                foreach (DataRowView drv in dv)
                {
                    Item i = new Item(Int32.Parse(drv["intID"].ToString()), drv["vcItemNumber"].ToString()
                        , drv["vcItemDescription"].ToString(), (drv["binImage"] != DBNull.Value ? (byte[])drv["binImage"] : null));
                    returnList.Add(i);
                }
                
                return returnList;
            }
            else
            {
                return new List<Item>();
            }

        }

        private bool ThumbnailCallback() { return false; }
        public byte[] GetResizedImage()
        {
    
            //Image thumb = i.GetThumbnailImage(128, 128, null, IntPtr.Zero);
            //thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            MemoryStream ms = new MemoryStream(this.Image);
            Image i = System.Drawing.Image.FromStream(ms);

            //Figure out what our heights and widths need to be
            //First lets find out resolution
            decimal res = (i.Height > i.Width ? ((decimal)i.Width / (decimal)i.Height) :((decimal)i.Height/(decimal)i.Width));
            int _height = i.Height;
            int _width = i.Width;

            if (_height > _width)
            {
                while (_height * res > 128)
                {
                    decimal value = (decimal)_height * res;
                    _height = (int)value;
                    value = (decimal)_width * res;
                    _width = (int)value;

                }
            }
            else
            {
                while (_width * res > 128)
                {
                    decimal value = (decimal)_width * res;
                    _width = (int)value;
                    value = (decimal)_height * res;
                    _height = (int)value;
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
        public byte[]? Image { get; protected set; }
        public int Status { get; protected set; }
        public int Quantity { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }
    }
}
