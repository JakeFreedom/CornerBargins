using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleOrders.Pages.Tools.AddItems
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostUploadMedia(IFormFile _file, string itemName, string itemDescription, string itemLabelColor, decimal itemCost, string itemLink) {
            System.Diagnostics.Debug.WriteLine(itemName);

            MemoryStream ms = new MemoryStream();
            _file.CopyToAsync(ms);
            string base64String = Convert.ToBase64String(ms.ToArray());
            Image = string.Format("data:image/" + "png" + ";base64,{0}", base64String);

            //Save this to the DB 
            Core.Item.AddItem(base64String, itemName, itemDescription, itemCost, itemLink, "green");
        }


        public string Image { get; protected set; }
    }
}
