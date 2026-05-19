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
            this.BinMedia = new BinaryMediaCollection();
            GetItems();
        }

        public void GetItems()
        {
            this.Items = Item.GetItems_NoMedia();

        }

        public void OnPostUpdateItem(int itemID)
        {
            System.Diagnostics.Debug.WriteLine(itemID);
            GetItems();
        }

        public void OnPostUpdateItemImage(IFormFile files, int itemID, bool binImage=false, int binImageID=0)
        {
            MemoryStream ms = new MemoryStream();
            files.CopyTo(ms);
            Core.Item.UpdateItemImage(ms.ToArray(), itemID);
            OnGet();
        }

        public void OnPostAddImage(IFormFile files, int itemID, int imageType)
        {
            //We need both images. The full image and the resized image. So we need to do this twice.


            Item i = new Item(itemID);
            byte[] newImage = i.GetResizedImage(350, 350);
            //Resize the image
            //Tag the Image Type
            //Save the image to the DB
            i.SaveImage(newImage);

        }
        public List<Item> Items { get; protected set; }
        public BinaryMediaCollection BinMedia { get; protected set; }
    }
}
