using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User
    {
        public static int CheckLogin(string strUserName, string strPassword)
        {
            if (strUserName.Length <= 0 || strPassword.Length <= 0)
                return 0;

            DataManager dm = new DataManager();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@vcUserName", strUserName);
            parameters.Add("@vcPassword", strPassword);

            dm.GetDataProc("User_CheckLogin", parameters);

            try
            {
                if(dm.RowsAffected>0)
                {
                    DataView dv = dm.GetDBDataAsDataView();
                    if(dv.Count>0)
                        return (int)dv[0][0];
                }

            }
            catch (Exception ex) { return 0; }

            return 0;
        }
    }
}

