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
            this.Items = Item.GetItems();

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
        public List<Item> Items { get; protected set; }
        public BinaryMediaCollection BinMedia { get; protected set; }
    }
}
