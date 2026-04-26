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

            string base64String = string.Empty;
            MemoryStream ms = new MemoryStream();
            if (itemDescription.Length > 0)
                itemDescription = itemDescription.Replace("\r\n", "<br />");
            else
                itemDescription = string.Empty;
            if (_file.Length > 0)
            {
                _file.CopyTo(ms);
                base64String = Convert.ToBase64String(ms.ToArray());
                Image = string.Format("data:image/" + "png" + ";base64,{0}", base64String);
            }

            //Save this to the DB 
            Core.Item.AddItem(ms.ToArray(), itemName, itemDescription, itemCost, itemLink, "green");
                
        }


        public string Image { get; protected set; }
    }
}
