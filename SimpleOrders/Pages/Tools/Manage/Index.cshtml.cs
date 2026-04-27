using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CB.Pages.Tools.Manage
{
    public class IndexModel : CB.Pages.Base.BasePageModel
    {
        public override void OnGet()
        {
            GetItems();
        }

        public void GetItems()
        {
            this.Items = Item.GetItems();

        }


        public List<Item> Items { get; protected set; }
    }
}
