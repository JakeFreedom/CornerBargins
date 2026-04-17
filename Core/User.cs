using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User
    {
        public static int CheckLogin(string strUserName, string strAccountID)
        {
            DataManager _dm = new DataManager();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@vcUserName", strUserName);
            parameters.Add("@vcCustomerID", strAccountID.ToString());

            _dm.GetDataProc("User_CheckLogin", parameters);

            try
            {
                object[] v = _dm.DBData.Tables[0].Rows[0].ItemArray;

                if (Int32.Parse(v.GetValue(0).ToString()) > 0)
                    return Int32.Parse(v.GetValue(0).ToString());
                else
                    return 0;
            }
            catch (Exception ex) { return 0; }
        }
    }
}

