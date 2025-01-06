using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core;

namespace SimpleOrders.Pages
{
    public class IndexModel : Base.BasePageModelModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public override void OnGet()
        {
            if(HttpContext.Session.GetString("isLoggedIn") == "true")
                Response.Redirect("/parts");   
        }

        public void OnGetCheckLogin(string strUserName)
        {
            if (Core.User.CheckLogin(strUserName))
            {
                HttpContext.Session.SetString("isLoggedIn", "true");
                HttpContext.Session.SetString("UserName", strUserName);
            }
        }
    }
}
