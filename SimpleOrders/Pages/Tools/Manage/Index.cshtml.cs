using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CB.Pages.Tools.Manage
{
    public class IndexModel : CB.Pages.Base.BasePageModel
    {
        public override void OnGet()
        {
            base.OnGet();
            //this.BinMedia = new BinaryMediaCollection();
            GetItems();
        }

        public void GetItems()
        {
            this.Items = Item.GetItems_NoMedia();

        }

        public void OnPostUpdateItem(int itemID)
        {
            GetItems();
        }

        public void OnPostUpdateItemImage(IFormFile files, int itemID, bool binImage=false, int binImageID=0)
        {
            MemoryStream ms = new MemoryStream();
            files.CopyTo(ms);
            Item.UpdateItemImage(ms.ToArray(), itemID);
            OnGet();
        }


        /// <summary>
        /// This is called from the tools where you can drag an image to the colored boxes right now.
        /// It should re-create image types 1,2 and 3
        /// </summary>
        /// <param name="files"></param>
        /// <param name="itemID"></param>
        /// <param name="imageType"></param>
        public void OnPostAddImage(IFormFile files, int itemID, int imageType)
        {
            //We need both images. The full image and the resized image. So we need to do this twice.
            MemoryStream ms = new MemoryStream();
            files.CopyTo(ms);
            Item i = new Item(itemID);
            byte[] newImage = i.GetFullImage(ms.ToArray(), 350, 350);

            //Resize the image
            //Tag the Image Type
            //Save the image to the DB
            //i.SaveImage(newImage);
            if(Item.AddItemImages(newImage, itemID)){
                System.Diagnostics.Debug.WriteLine("All Images saved");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Failed saving images");
            }

        }
        public List<Item> Items { get; protected set; }
        public BinaryMediaCollection BinMedia { get; protected set; }

        public MediaCollection ItemIcons { get { return new MediaCollection(); } protected set { ItemIcons = value; } }
    }
}
