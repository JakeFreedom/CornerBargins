using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleOrders.Pages.Base
{
    public class BasePageModelModel : PageModel
    {
        public virtual void OnGet()
        {
            //CheckForLogin()
        }
    }
}
