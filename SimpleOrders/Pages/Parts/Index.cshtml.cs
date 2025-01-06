using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleOrders.Pages.Parts
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {

            this.UserName = HttpContext.Session.GetString("UserName");
        }



        public string UserName { get; protected set; }
    }
}
