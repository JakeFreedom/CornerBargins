using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CB.Pages.Base
{
    public class BasePageModel : PageModel
    {
        public virtual void OnGet()
        {
            CheckValidUser();
        }
        
        public void CheckValidUser()
        {
            //System.Diagnostics.Debug.WriteLine("Checking for valid user");
            //CheckForLogin()
            //System.Diagnostics.Debug.WriteLine(HttpContext.Request.GetDisplayUrl());
            if (HttpContext.Session.Keys.Count() == 0)
            {
                //System.Diagnostics.Debug.WriteLine(HttpContext.Request.Host);// GetDisplayUrl());
                HttpContext.Response.Redirect($"https://{HttpContext.Request.Host}/Login");
            }
            else
            {
                this.IsValidUser = true;
                this.UserID = (int)HttpContext.Session.GetInt32("UserID");
                this.UserName = HttpContext.Session.GetString("UserName");

            }
        }

        public int UserID { get; protected set; }
        public string UserName { get; protected set; }  
        public bool IsValidUser { get; protected set; }
        public int AccountID { get; protected set; }
    }
}
