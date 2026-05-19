using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class Media
	{
		public Media() { }
		public Media(int id) {
			this.ImageID = id;
			LoadMedia();
		}
		public Media(int id, byte[] image, int imageType, int imageSize, string imageName, DateTime created, DateTime updated, string itemGUID)
		{
			ImageID = id;
			Image = image;
			ImageType = imageType;
			ImageSize = imageSize;
			ImageName = imageName;
			Created = created;
			Updated = updated;
			ItemGUID = itemGUID;
		}

		private void LoadMedia()
		{
			DataManager db = new DataManager();
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("@intImageID", this.ImageID.ToString());
			db.GetDataProc("Media_SBy_ID", parameters);
			DataView dv = db.GetDBDataAsDataView();
			if (dv.Count > 0)
			{
				DataRowView drv = dv[0];
				this.Image = (byte[])drv["binImage"];
			}
		}

		public int ImageID { get; protected set; }
		public byte[] Image { get; protected set; }
		public int ImageType { get; protected set; }
		public int ImageSize{ get; protected set; }
		public string ImageName { get; protected set; }
		public string ItemGUID { get; protected set; }
		public DateTime Created { get; protected set; }
		public DateTime Updated { get; protected set; }
	}

	public class MediaCollection : CollectionBase {

		public MediaCollection() { }
		public MediaCollection(int itemID) 
		{

			DataManager dm = new DataManager();
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("@intItemID", itemID.ToString());
			dm.GetDataProc("Media_SBy_ItemID", parameters);
			if (dm.RowsAffected > 0)
			{
				DataView dv = dm.GetDBDataAsDataView();
				if (dv.Count > 0)
				{
					foreach (DataRowView drv in dv)
					{
						Media m = new Media(itemID, (byte[])drv["binImage"], (int)drv["intImageType"], (int)drv["intFileSize"], "", 
							DateTime.Parse(drv["dtCreated"].ToString()), DateTime.Parse(drv["dtUpdated"].ToString()), drv["itemGUID"].ToString())
						{
							
						};
						this.List.Add(m);
					}

				}
			}
		}

		public MediaCollection(int itemID, ENUMS.ImageType imageType){

			DataManager dm = new DataManager();
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("@intItemID", itemID.ToString());
			parameters.Add("@intImageType", imageType.ToString());
			dm.GetDataProc("Media_SBy_ItemIDImageType", parameters);
			if (dm.RowsAffected > 0)
			{
				DataView dv = dm.GetDBDataAsDataView();
				if (dv.Count > 0)
				{
					foreach (DataRowView drv in dv)
					{
						Media m = new Media(itemID, (byte[])drv["binImage"], (int)drv["intImageType"], (int)drv["intFileSize"], "",
							DateTime.Parse(drv["dtCreated"].ToString()), DateTime.Parse(drv["dtUpdated"].ToString()), drv["itemGUID"].ToString())
						{

						};
						this.List.Add(m);
					}

				}
			}
		}
		public MediaCollection(Media m) { }
	}
}
