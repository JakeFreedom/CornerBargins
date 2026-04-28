using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CB.Pages.Login
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostCheckLogin(string UserName, string Password)
        {
            //System.Security.Cryptography.Aes.Create();
            //System.Diagnostics.Debug.WriteLine(UserName);
            //System.Diagnostics.Debug.WriteLine(Password);
            //var (k, iv) = Core.CryptoServices.GenerateKeyAndIv();
            //string passToSend = Core.CryptoServices.EncryptString(Password,k,iv);
            string passToSend = Core.CryptoServices.HashString(Password + UserName);
            int id = Core.User.CheckLogin(UserName, passToSend);
            if(id>0)
            {
                HttpContext.Session.SetInt32("UserID", id);
                HttpContext.Session.SetString("isLoggedIn", "true");
                HttpContext.Session.SetString("UserName", UserName);
                //HttpContext.Response.Redirect("https://localhost:44305/Tools/Manage");
            }
        }
    }
}
