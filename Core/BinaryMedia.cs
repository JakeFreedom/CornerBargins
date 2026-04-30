using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BinaryMedia
    {
        public BinaryMedia() { }
        public BinaryMedia(int BinaryMediaID) { this.BinaryMediaID = BinaryMediaID; }
        public BinaryMedia(int BinaryMediaID, byte[] Image, string FileName, string FileExtension, int FileSize, string GUID, DateTime Created, DateTime Updated)
        {
            this.BinaryMediaID = BinaryMediaID;
            this.Image = Image;
            this.FileName = FileName;
            this.FileExtension = FileExtension;
            this.FileSize = FileSize;
            this.GUID = GUID;
            this.Created = Created;
            this.Updated = Updated;
        }


        public int BinaryMediaID { get; set; }  
        public byte[] Image { get; set; }   
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int FileSize { get; set; }
        public string GUID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class BinaryMediaCollection: CollectionBase 
    { 

        public int GetBinaryMediaByItem(int ItemID)
        {
            //SP GetBinaryMediaByItem
            //foreach item in dv instantiate new BinaryMedia and add to this collection
            DataManager dm = new DataManager();
            Dictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add("itemID", ItemID.ToString());
            dm.GetDataProc("GetBinaryMediaByItem", parameters);
            DataView dv = dm.GetDBDataAsDataView();
            BinaryMedia media = null;
            foreach(DataRowView drv in dv)
            {
                media = new BinaryMedia(Int32.Parse(drv["intID"].ToString()), (byte[])drv["binImage"], "", "", 0, drv["itemGUID"].ToString(), DateTime.Parse(drv["dtCreated"].ToString()), DateTime.Parse(drv["dtUpdated"].ToString()));
                this.List.Add(media);  
            }
            
            return this.List.Count;
        }

        public BinaryMedia this[int index] {
            get => this[index];
            set => value = this[index];
        }
    }
}
