using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User
    {
        public static bool CheckLogin(string UserName)
        {
            DataManager _dm = new DataManager();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@vcUserName", UserName);

            _dm.GetDataProc("User_CheckLogin", parameters);

            object[] v = _dm.DBData.Tables[0].Rows[0].ItemArray;
            
            if((int)v.GetValue(0) == 1)
                return true;
            else
                return false;
        }
    }
}
